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
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    Class = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
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
                    { 1, 25, 2, "[\"przesz\\u0142ym prostym\",\"przysz\\u0142ym z\\u0142o\\u017Conym\",\"przesz\\u0142ym z\\u0142o\\u017Conym\",\"przesz\\u0142ym pomocniczym\"]", "Czas Perfekt jest czasem:" },
                    { 2, 25, 1, "[\"po czasowniku sein/haben\",\"na drugim miejscu\",\"przed podmiotem\",\"na ostatnim miejscu\"]", "Czasownik pomocniczy występuje:" },
                    { 3, 25, 3, "[\"tylko sein\",\"machen\",\"tylko haben\",\"sein i haben\"]", "Czasownikiem pomocniczym jest:" },
                    { 4, 25, 2, "[\"Genitiv (kogo? czego? czyj?)\",\"Dativ (komu? czemu?)\",\"Akkusativ (kogo? co?)\",\"Nominativ (kto? co?)\"]", "Jaki przypadek zawsze wymaga haben jako czasownika pomocniczego?" },
                    { 5, 25, 2, "[\"czasownik bez -en \\u002B t\",\"ge \\u002B czasownik\",\"ge \\u002B czasownik bez -en \\u002B t\",\"ge \\u002B czasownik bez -en\"]", "W jaki sposób tworzymy regularne czasowniki Partizip II?" },
                    { 6, 25, 2, "[\"kiedy nast\\u0119puje zmiana stanu\",\"kiedy czasownik okre\\u015Bla ruch\",\"kiedy czasownik jest zwrotny\",\"po czasownikach sein, bleiben i werden\"]", "Kiedy NIE dajemy sein?" },
                    { 7, 25, 0, "[\"sich erholen\",\"fliegen\",\"gehen\",\"aufwachen\"]", "Wskaż czasownik łączący się z haben" },
                    { 8, 25, 0, "[\"sein\",\"kochen\",\"machen\",\"denken\"]", "Wskaż czasownik łączący się z sein" },
                    { 9, 25, 2, "[\"Ich bin viel geschlafen\",\"Hast du zu Hause geblieben?\",\"Haben Sie einen Termin beim Doktor Jacha\\u015B vereinbart?\",\"Hast du in Polen gewandert?\"]", "Wskaż poprawne zdanie" },
                    { 10, 25, 2, "[\"Was bist du am Wochenende gemacht?\",\"Bist du diese Leute schon getroffen?\",\"Ich habe gestern viele Kekse gebacken\",\"Ich bin meinem Vater zum Bahnhof gefahren\"]", "Wskaż poprawne zdanie" },
                    { 11, 25, 2, "[\"Sie ist ihre Hausaufgaben gemacht\",\"Du hast am Morgen viel galaufen\",\"Ich habe meinem Bruder zur Schule gefahren\",\"Ich habe in der Schule gegangen\"]", "Wskaż poprawne zdanie" },
                    { 12, 25, 2, "[\"Wir sind uns erholt\",\"Ihr habt ins Kino gefahren\",\"Ich bin vor drei Monaten nach Italien geflogen\",\"Sie sind drei Stunden gearbeitet\"]", "Wskaż poprawne zdanie" },
                    { 13, 25, 0, "[\"gemacht\",\"machen\",\"gemach\",\"gemachen\"]", "Wybierz właściwą formę Partizip II czasownika machen" },
                    { 14, 25, 1, "[\"gearbeit\",\"gearbeitet\",\"gearbeitt\",\"gearbeiten\"]", "Wybierz właściwą formę Partizip II czasownika arbeiten" },
                    { 15, 25, 2, "[\"aufgewachen\",\"geaufwachen\",\"aufgewacht\",\"geaufwacht\"]", "Wybierz właściwą formę Partizip II czasownika aufwachen" },
                    { 16, 25, 2, "[\"verkaufen\",\"geverkaufen\",\"verkauft\",\"vergekauft\"]", "Wybierz właściwą formę Partizip II czasownika verkaufen" },
                    { 17, 25, 1, "[\"ist telefoniert\",\"hat telefoniert\",\"ist telefonieren\",\"hat getelefoniert\"]", "Wybierz właściwy czasownik pomocniczy i formę Partizip II czasownika telefonieren" },
                    { 18, 25, 2, "[\"ist gegehen\",\"hat gegehen\",\"ist gegangen\",\"hat gegeht\"]", "Wybierz właściwy czasownik pomocniczy i formę Partizip II czasownika gehen" },
                    { 19, 25, 2, "[\"hat aufgestanden\",\"ist geaufstehen\",\"ist aufgestanden\",\"hat geaufstehen\"]", "Wybierz właściwy czasownik pomocniczy i formę Partizip II czasownika aufstehen" },
                    { 20, 25, 1, "[\"hat geeinschlafen\",\"ist eingeschlafen\",\"ist geeinschlafen\",\"hat eingeschlaft\"]", "Wybierz właściwy czasownik pomocniczy i formę Partizip II czasownika einschlafen" }
                });

            migrationBuilder.InsertData(
                table: "SystemStatusEntities",
                columns: new[] { "Id", "StartTime", "Status", "UpdatedAt" },
                values: new object[] { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, new DateTime(2024, 4, 13, 13, 10, 41, 663, DateTimeKind.Utc).AddTicks(9084) });

            migrationBuilder.InsertData(
                table: "UserEntities",
                columns: new[] { "Id", "AccountType", "Class", "EndTime", "Login", "Name", "Password", "Status", "Surname" },
                values: new object[,]
                {
                    { 1, 1, "3TP", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin.admin", "admin", "0", 0, "admin" },
                    { 2, 0, "17", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "seumas.baxill", "Seumas", "422", 0, "Baxill" },
                    { 3, 0, "66", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "mack.waplinton", "Mack", "755", 0, "Waplinton" },
                    { 4, 0, "46", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "janetta.mcgraith", "Janetta", "241", 0, "McGraith" },
                    { 5, 0, "41", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "martita.bovey", "Martita", "547", 0, "Bovey" },
                    { 6, 0, "51", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "persis.elias", "Persis", "118", 0, "Elias" },
                    { 7, 0, "66", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "carine.golightly", "Carine", "252", 0, "Golightly" },
                    { 8, 0, "30", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ann-marie.josipovitz", "Ann-marie", "219", 0, "Josipovitz" },
                    { 9, 0, "18", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "liane.glendinning", "Liane", "626", 0, "Glendinning" },
                    { 10, 0, "56", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "garland.kindell", "Garland", "765", 0, "Kindell" },
                    { 11, 0, "86", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "cosetta.lassey", "Cosetta", "124", 0, "Lassey" },
                    { 12, 0, "28", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "thomasine.quelch", "Thomasine", "624", 0, "Quelch" },
                    { 13, 0, "17", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "maximilian.pidler", "Maximilian", "166", 0, "Pidler" },
                    { 14, 0, "70", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "leonard.letch", "Leonard", "834", 0, "Letch" },
                    { 15, 0, "15", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "tiena.matteini", "Tiena", "514", 0, "Matteini" },
                    { 16, 0, "71", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "krystal.seman", "Krystal", "459", 0, "Seman" },
                    { 17, 0, "42", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "marcela.peperell", "Marcela", "539", 0, "Peperell" },
                    { 18, 0, "72", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "gerik.melarkey", "Gerik", "178", 0, "Melarkey" },
                    { 19, 0, "51", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "rose.tousy", "Rose", "148", 0, "Tousy" },
                    { 20, 0, "93", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "jordana.dalzell", "Jordana", "321", 0, "Dalzell" },
                    { 21, 0, "80", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "silvio.hanselman", "Silvio", "722", 0, "Hanselman" },
                    { 22, 0, "49", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "harriett.twittey", "Harriett", "687", 0, "Twittey" },
                    { 23, 0, "14", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "burty.jessop", "Burty", "745", 0, "Jessop" },
                    { 24, 0, "83", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "isiahi.mandrake", "Isiahi", "318", 0, "Mandrake" },
                    { 25, 0, "28", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "jammal.snare", "Jammal", "657", 0, "Snare" },
                    { 26, 0, "69", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "crawford.davoren", "Crawford", "919", 0, "Davoren" },
                    { 27, 0, "61", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "livvie.doche", "Livvie", "627", 0, "Doche" },
                    { 28, 0, "39", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "elfreda.lafee", "Elfreda", "784", 0, "Lafee" },
                    { 29, 0, "71", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "hadleigh.fishbourn", "Hadleigh", "984", 0, "Fishbourn" },
                    { 30, 0, "69", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "deanne.warboys", "Deanne", "919", 0, "Warboys" },
                    { 31, 0, "81", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ailbert.handasyde", "Ailbert", "124", 0, "Handasyde" },
                    { 32, 0, "42", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "guenna.o'hengerty", "Guenna", "483", 0, "O'Hengerty" },
                    { 33, 0, "88", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "arluene.figurski", "Arluene", "765", 0, "Figurski" },
                    { 34, 0, "76", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "wendall.guswell", "Wendall", "225", 0, "Guswell" },
                    { 35, 0, "78", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "raye.schreurs", "Raye", "181", 0, "Schreurs" },
                    { 36, 0, "65", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "zach.de filippo", "Zach", "329", 0, "De Filippo" },
                    { 37, 0, "43", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "karina.hailes", "Karina", "334", 0, "Hailes" },
                    { 38, 0, "55", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "orly.pleasaunce", "Orly", "252", 0, "Pleasaunce" },
                    { 39, 0, "82", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "peter.klain", "Peter", "921", 0, "Klain" },
                    { 40, 0, "18", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "peta.aspland", "Peta", "815", 0, "Aspland" },
                    { 41, 0, "28", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "norbie.baldree", "Norbie", "636", 0, "Baldree" },
                    { 42, 0, "47", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ethyl.kneel", "Ethyl", "927", 0, "Kneel" },
                    { 43, 0, "52", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "lissy.theml", "Lissy", "641", 0, "Theml" },
                    { 44, 0, "33", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "felipe.dinneen", "Felipe", "154", 0, "Dinneen" },
                    { 45, 0, "85", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "abra.coad", "Abra", "172", 0, "Coad" },
                    { 46, 0, "12", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "dill.jales", "Dill", "348", 0, "Jales" },
                    { 47, 0, "90", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "jackelyn.broadwell", "Jackelyn", "571", 0, "Broadwell" },
                    { 48, 0, "90", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ines.rome", "Ines", "327", 0, "Rome" },
                    { 49, 0, "54", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "laryssa.maughan", "Laryssa", "364", 0, "Maughan" },
                    { 50, 0, "95", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "marcela.loweth", "Marcela", "519", 0, "Loweth" },
                    { 51, 0, "80", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "abraham.errichelli", "Abraham", "486", 0, "Errichelli" },
                    { 52, 0, "11", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "cristionna.leyland", "Cristionna", "738", 0, "Leyland" },
                    { 53, 0, "100", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "humberto.routham", "Humberto", "599", 0, "Routham" },
                    { 54, 0, "76", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "joice.lipp", "Joice", "823", 0, "Lipp" },
                    { 55, 0, "6", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "barbe.kennaird", "Barbe", "655", 0, "Kennaird" },
                    { 56, 0, "1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ignaz.erett", "Ignaz", "574", 0, "Erett" },
                    { 57, 0, "67", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "patience.tunmore", "Patience", "953", 0, "Tunmore" },
                    { 58, 0, "79", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "costa.murrigan", "Costa", "962", 0, "Murrigan" },
                    { 59, 0, "66", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "pedro.stetson", "Pedro", "128", 0, "Stetson" },
                    { 60, 0, "96", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "palm.cranmore", "Palm", "262", 0, "Cranmore" },
                    { 61, 0, "61", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "emilio.fergie", "Emilio", "188", 0, "Fergie" },
                    { 62, 0, "50", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ericka.o'heyne", "Ericka", "374", 0, "O'Heyne" },
                    { 63, 0, "85", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "hazel.mcsarry", "Hazel", "516", 0, "Mcsarry" },
                    { 64, 0, "67", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "mirabella.le fleming", "Mirabella", "173", 0, "Le Fleming" },
                    { 65, 0, "39", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ilyse.kanter", "Ilyse", "366", 0, "Kanter" },
                    { 66, 0, "71", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "mychal.guidone", "Mychal", "848", 0, "Guidone" },
                    { 67, 0, "22", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "rosie.tuckerman", "Rosie", "845", 0, "Tuckerman" },
                    { 68, 0, "56", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "erroll.camlin", "Erroll", "333", 0, "Camlin" },
                    { 69, 0, "35", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "chad.ruprecht", "Chad", "286", 0, "Ruprecht" },
                    { 70, 0, "4", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "taber.zincke", "Taber", "413", 0, "Zincke" },
                    { 71, 0, "65", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "katheryn.d'escoffier", "Katheryn", "575", 0, "d'Escoffier" },
                    { 72, 0, "42", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "genovera.augustin", "Genovera", "938", 0, "Augustin" },
                    { 73, 0, "74", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "benjy.cowlard", "Benjy", "719", 0, "Cowlard" },
                    { 74, 0, "76", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ralina.garraway", "Ralina", "937", 0, "Garraway" },
                    { 75, 0, "40", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "heather.desforges", "Heather", "366", 0, "Desforges" },
                    { 76, 0, "51", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "tersina.cancott", "Tersina", "681", 0, "Cancott" },
                    { 77, 0, "6", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "loy.martinuzzi", "Loy", "536", 0, "Martinuzzi" },
                    { 78, 0, "36", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "dill.eames", "Dill", "422", 0, "Eames" },
                    { 79, 0, "52", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "dell.hardway", "Dell", "528", 0, "Hardway" },
                    { 80, 0, "52", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "erick.floyd", "Erick", "778", 0, "Floyd" },
                    { 81, 0, "53", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "helenelizabeth.scoffins", "Helenelizabeth", "691", 0, "Scoffins" },
                    { 82, 0, "95", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "felix.pudner", "Felix", "945", 0, "Pudner" },
                    { 83, 0, "99", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "nydia.ritch", "Nydia", "172", 0, "Ritch" },
                    { 84, 0, "91", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "sheffie.bonnick", "Sheffie", "874", 0, "Bonnick" },
                    { 85, 0, "70", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "dominick.evennett", "Dominick", "728", 0, "Evennett" },
                    { 86, 0, "74", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "peirce.kembley", "Peirce", "872", 0, "Kembley" },
                    { 87, 0, "74", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "torrance.gianettini", "Torrance", "266", 0, "Gianettini" },
                    { 88, 0, "68", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "bryce.sutherden", "Bryce", "918", 0, "Sutherden" },
                    { 89, 0, "71", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "alexandra.mckeney", "Alexandra", "489", 0, "McKeney" },
                    { 90, 0, "82", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "paul.grieger", "Paul", "124", 0, "Grieger" },
                    { 91, 0, "11", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "prudi.torns", "Prudi", "197", 0, "Torns" },
                    { 92, 0, "75", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "madonna.costelow", "Madonna", "429", 0, "Costelow" },
                    { 93, 0, "96", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "brandie.luparti", "Brandie", "118", 0, "Luparti" },
                    { 94, 0, "89", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ky.epelett", "Ky", "986", 0, "Epelett" },
                    { 95, 0, "44", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "hester.peracco", "Hester", "555", 0, "Peracco" },
                    { 96, 0, "60", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "gill.starbucke", "Gill", "371", 0, "Starbucke" },
                    { 97, 0, "23", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "jerrie.stoter", "Jerrie", "775", 0, "Stoter" },
                    { 98, 0, "13", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "urbanus.fancet", "Urbanus", "796", 0, "Fancet" },
                    { 99, 0, "74", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "michaelina.mcbeith", "Michaelina", "986", 0, "McBeith" },
                    { 100, 0, "41", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "fedora.sonier", "Fedora", "641", 0, "Sonier" },
                    { 101, 0, "61", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "berenice.sewter", "Berenice", "868", 0, "Sewter" }
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
