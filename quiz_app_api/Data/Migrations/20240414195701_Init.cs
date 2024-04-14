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
                    AvailableTime = table.Column<int>(type: "int", nullable: false),
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
                    { 1, 25, 0, "[\"przesz\\u0142ym z\\u0142o\\u017Conym\",\"przysz\\u0142ym z\\u0142o\\u017Conym\",\"przesz\\u0142ym prostym\",\"przesz\\u0142ym pomocniczym\"]", "Czas Perfekt jest czasem:" },
                    { 2, 25, 1, "[\"przed podmiotem\",\"na drugim miejscu\",\"po czasowniku sein/haben\",\"na ostatnim miejscu\"]", "Czasownik pomocniczy występuje:" },
                    { 3, 25, 0, "[\"sein i haben\",\"tylko sein\",\"tylko haben\",\"machen\"]", "Czasownikiem pomocniczym jest:" },
                    { 4, 25, 2, "[\"Dativ (komu? czemu?)\",\"Genitiv (kogo? czego? czyj?)\",\"Akkusativ (kogo? co?)\",\"Nominativ (kto? co?)\"]", "Jaki przypadek zawsze wymaga haben jako czasownika pomocniczego?" },
                    { 5, 25, 2, "[\"czasownik bez -en \\u002B t\",\"ge \\u002B czasownik bez -en\",\"ge \\u002B czasownik bez -en \\u002B t\",\"ge \\u002B czasownik\"]", "W jaki sposób tworzymy regularne czasowniki Partizip II?" },
                    { 6, 25, 2, "[\"kiedy czasownik okre\\u015Bla ruch\",\"po czasownikach sein, bleiben i werden\",\"kiedy czasownik jest zwrotny\",\"kiedy nast\\u0119puje zmiana stanu\"]", "Kiedy NIE dajemy sein?" },
                    { 7, 25, 0, "[\"sich erholen\",\"fliegen\",\"aufwachen\",\"gehen\"]", "Wskaż czasownik łączący się z haben" },
                    { 8, 25, 1, "[\"denken\",\"sein\",\"machen\",\"kochen\"]", "Wskaż czasownik łączący się z sein" },
                    { 9, 25, 0, "[\"Haben Sie einen Termin beim Doktor Jacha\\u015B vereinbart?\",\"Hast du in Polen gewandert?\",\"Hast du zu Hause geblieben?\",\"Ich bin viel geschlafen\"]", "Wskaż poprawne zdanie" },
                    { 10, 25, 1, "[\"Bist du diese Leute schon getroffen?\",\"Ich habe gestern viele Kekse gebacken\",\"Was bist du am Wochenende gemacht?\",\"Ich bin meinem Vater zum Bahnhof gefahren\"]", "Wskaż poprawne zdanie" },
                    { 11, 25, 3, "[\"Du hast am Morgen viel galaufen\",\"Ich habe in der Schule gegangen\",\"Sie ist ihre Hausaufgaben gemacht\",\"Ich habe meinem Bruder zur Schule gefahren\"]", "Wskaż poprawne zdanie" },
                    { 12, 25, 0, "[\"Ich bin vor drei Monaten nach Italien geflogen\",\"Ihr habt ins Kino gefahren\",\"Sie sind drei Stunden gearbeitet\",\"Wir sind uns erholt\"]", "Wskaż poprawne zdanie" },
                    { 13, 25, 3, "[\"gemachen\",\"machen\",\"gemach\",\"gemacht\"]", "Wybierz właściwą formę Partizip II czasownika machen" },
                    { 14, 25, 1, "[\"gearbeit\",\"gearbeitet\",\"gearbeitt\",\"gearbeiten\"]", "Wybierz właściwą formę Partizip II czasownika arbeiten" },
                    { 15, 25, 2, "[\"geaufwacht\",\"aufgewachen\",\"aufgewacht\",\"geaufwachen\"]", "Wybierz właściwą formę Partizip II czasownika aufwachen" },
                    { 16, 25, 2, "[\"geverkaufen\",\"vergekauft\",\"verkauft\",\"verkaufen\"]", "Wybierz właściwą formę Partizip II czasownika verkaufen" },
                    { 17, 25, 1, "[\"hat getelefoniert\",\"hat telefoniert\",\"ist telefonieren\",\"ist telefoniert\"]", "Wybierz właściwy czasownik pomocniczy i formę Partizip II czasownika telefonieren" },
                    { 18, 25, 1, "[\"ist gegehen\",\"ist gegangen\",\"hat gegeht\",\"hat gegehen\"]", "Wybierz właściwy czasownik pomocniczy i formę Partizip II czasownika gehen" },
                    { 19, 25, 2, "[\"hat geaufstehen\",\"ist geaufstehen\",\"ist aufgestanden\",\"hat aufgestanden\"]", "Wybierz właściwy czasownik pomocniczy i formę Partizip II czasownika aufstehen" },
                    { 20, 25, 3, "[\"hat eingeschlaft\",\"ist geeinschlafen\",\"hat geeinschlafen\",\"ist eingeschlafen\"]", "Wybierz właściwy czasownik pomocniczy i formę Partizip II czasownika einschlafen" }
                });

            migrationBuilder.InsertData(
                table: "SystemStatusEntities",
                columns: new[] { "Id", "AvailableTime", "StartTime", "Status", "UpdatedAt" },
                values: new object[] { 1, 45, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, new DateTime(2024, 4, 14, 19, 57, 0, 449, DateTimeKind.Utc).AddTicks(2502) });

            migrationBuilder.InsertData(
                table: "UserEntities",
                columns: new[] { "Id", "AccountType", "Class", "EndTime", "Login", "Name", "Password", "Status", "Surname" },
                values: new object[,]
                {
                    { 1, 1, "3TP", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin.admin", "admin", "0", 0, "admin" },
                    { 2, 0, "17", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "seumas.baxill", "Seumas", "967", 0, "Baxill" },
                    { 3, 0, "66", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "mack.waplinton", "Mack", "714", 0, "Waplinton" },
                    { 4, 0, "46", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "janetta.mcgraith", "Janetta", "411", 0, "McGraith" },
                    { 5, 0, "41", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "martita.bovey", "Martita", "222", 0, "Bovey" },
                    { 6, 0, "51", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "persis.elias", "Persis", "769", 0, "Elias" },
                    { 7, 0, "66", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "carine.golightly", "Carine", "782", 0, "Golightly" },
                    { 8, 0, "30", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ann-marie.josipovitz", "Ann-marie", "138", 0, "Josipovitz" },
                    { 9, 0, "18", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "liane.glendinning", "Liane", "864", 0, "Glendinning" },
                    { 10, 0, "56", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "garland.kindell", "Garland", "628", 0, "Kindell" },
                    { 11, 0, "86", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "cosetta.lassey", "Cosetta", "934", 0, "Lassey" },
                    { 12, 0, "28", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "thomasine.quelch", "Thomasine", "813", 0, "Quelch" },
                    { 13, 0, "17", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "maximilian.pidler", "Maximilian", "622", 0, "Pidler" },
                    { 14, 0, "70", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "leonard.letch", "Leonard", "133", 0, "Letch" },
                    { 15, 0, "15", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "tiena.matteini", "Tiena", "332", 0, "Matteini" },
                    { 16, 0, "71", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "krystal.seman", "Krystal", "582", 0, "Seman" },
                    { 17, 0, "42", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "marcela.peperell", "Marcela", "823", 0, "Peperell" },
                    { 18, 0, "72", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "gerik.melarkey", "Gerik", "173", 0, "Melarkey" },
                    { 19, 0, "51", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "rose.tousy", "Rose", "831", 0, "Tousy" },
                    { 20, 0, "93", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "jordana.dalzell", "Jordana", "497", 0, "Dalzell" },
                    { 21, 0, "80", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "silvio.hanselman", "Silvio", "443", 0, "Hanselman" },
                    { 22, 0, "49", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "harriett.twittey", "Harriett", "483", 0, "Twittey" },
                    { 23, 0, "14", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "burty.jessop", "Burty", "285", 0, "Jessop" },
                    { 24, 0, "83", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "isiahi.mandrake", "Isiahi", "188", 0, "Mandrake" },
                    { 25, 0, "28", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "jammal.snare", "Jammal", "214", 0, "Snare" },
                    { 26, 0, "69", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "crawford.davoren", "Crawford", "318", 0, "Davoren" },
                    { 27, 0, "61", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "livvie.doche", "Livvie", "752", 0, "Doche" },
                    { 28, 0, "39", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "elfreda.lafee", "Elfreda", "968", 0, "Lafee" },
                    { 29, 0, "71", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "hadleigh.fishbourn", "Hadleigh", "288", 0, "Fishbourn" },
                    { 30, 0, "69", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "deanne.warboys", "Deanne", "474", 0, "Warboys" },
                    { 31, 0, "81", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ailbert.handasyde", "Ailbert", "387", 0, "Handasyde" },
                    { 32, 0, "42", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "guenna.o'hengerty", "Guenna", "576", 0, "O'Hengerty" },
                    { 33, 0, "88", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "arluene.figurski", "Arluene", "752", 0, "Figurski" },
                    { 34, 0, "76", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "wendall.guswell", "Wendall", "646", 0, "Guswell" },
                    { 35, 0, "78", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "raye.schreurs", "Raye", "973", 0, "Schreurs" },
                    { 36, 0, "65", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "zach.de filippo", "Zach", "237", 0, "De Filippo" },
                    { 37, 0, "43", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "karina.hailes", "Karina", "148", 0, "Hailes" },
                    { 38, 0, "55", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "orly.pleasaunce", "Orly", "735", 0, "Pleasaunce" },
                    { 39, 0, "82", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "peter.klain", "Peter", "377", 0, "Klain" },
                    { 40, 0, "18", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "peta.aspland", "Peta", "235", 0, "Aspland" },
                    { 41, 0, "28", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "norbie.baldree", "Norbie", "368", 0, "Baldree" },
                    { 42, 0, "47", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ethyl.kneel", "Ethyl", "982", 0, "Kneel" },
                    { 43, 0, "52", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "lissy.theml", "Lissy", "242", 0, "Theml" },
                    { 44, 0, "33", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "felipe.dinneen", "Felipe", "442", 0, "Dinneen" },
                    { 45, 0, "85", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "abra.coad", "Abra", "826", 0, "Coad" },
                    { 46, 0, "12", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "dill.jales", "Dill", "627", 0, "Jales" },
                    { 47, 0, "90", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "jackelyn.broadwell", "Jackelyn", "989", 0, "Broadwell" },
                    { 48, 0, "90", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ines.rome", "Ines", "455", 0, "Rome" },
                    { 49, 0, "54", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "laryssa.maughan", "Laryssa", "136", 0, "Maughan" },
                    { 50, 0, "95", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "marcela.loweth", "Marcela", "271", 0, "Loweth" },
                    { 51, 0, "80", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "abraham.errichelli", "Abraham", "945", 0, "Errichelli" },
                    { 52, 0, "11", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "cristionna.leyland", "Cristionna", "628", 0, "Leyland" },
                    { 53, 0, "100", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "humberto.routham", "Humberto", "273", 0, "Routham" },
                    { 54, 0, "76", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "joice.lipp", "Joice", "582", 0, "Lipp" },
                    { 55, 0, "6", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "barbe.kennaird", "Barbe", "738", 0, "Kennaird" },
                    { 56, 0, "1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ignaz.erett", "Ignaz", "278", 0, "Erett" },
                    { 57, 0, "67", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "patience.tunmore", "Patience", "594", 0, "Tunmore" },
                    { 58, 0, "79", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "costa.murrigan", "Costa", "822", 0, "Murrigan" },
                    { 59, 0, "66", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "pedro.stetson", "Pedro", "814", 0, "Stetson" },
                    { 60, 0, "96", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "palm.cranmore", "Palm", "625", 0, "Cranmore" },
                    { 61, 0, "61", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "emilio.fergie", "Emilio", "922", 0, "Fergie" },
                    { 62, 0, "50", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ericka.o'heyne", "Ericka", "953", 0, "O'Heyne" },
                    { 63, 0, "85", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "hazel.mcsarry", "Hazel", "755", 0, "Mcsarry" },
                    { 64, 0, "67", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "mirabella.le fleming", "Mirabella", "676", 0, "Le Fleming" },
                    { 65, 0, "39", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ilyse.kanter", "Ilyse", "986", 0, "Kanter" },
                    { 66, 0, "71", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "mychal.guidone", "Mychal", "296", 0, "Guidone" },
                    { 67, 0, "22", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "rosie.tuckerman", "Rosie", "969", 0, "Tuckerman" },
                    { 68, 0, "56", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "erroll.camlin", "Erroll", "666", 0, "Camlin" },
                    { 69, 0, "35", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "chad.ruprecht", "Chad", "667", 0, "Ruprecht" },
                    { 70, 0, "4", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "taber.zincke", "Taber", "964", 0, "Zincke" },
                    { 71, 0, "65", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "katheryn.d'escoffier", "Katheryn", "348", 0, "d'Escoffier" },
                    { 72, 0, "42", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "genovera.augustin", "Genovera", "285", 0, "Augustin" },
                    { 73, 0, "74", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "benjy.cowlard", "Benjy", "345", 0, "Cowlard" },
                    { 74, 0, "76", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ralina.garraway", "Ralina", "329", 0, "Garraway" },
                    { 75, 0, "40", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "heather.desforges", "Heather", "943", 0, "Desforges" },
                    { 76, 0, "51", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "tersina.cancott", "Tersina", "315", 0, "Cancott" },
                    { 77, 0, "6", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "loy.martinuzzi", "Loy", "644", 0, "Martinuzzi" },
                    { 78, 0, "36", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "dill.eames", "Dill", "664", 0, "Eames" },
                    { 79, 0, "52", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "dell.hardway", "Dell", "394", 0, "Hardway" },
                    { 80, 0, "52", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "erick.floyd", "Erick", "532", 0, "Floyd" },
                    { 81, 0, "53", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "helenelizabeth.scoffins", "Helenelizabeth", "558", 0, "Scoffins" },
                    { 82, 0, "95", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "felix.pudner", "Felix", "927", 0, "Pudner" },
                    { 83, 0, "99", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "nydia.ritch", "Nydia", "977", 0, "Ritch" },
                    { 84, 0, "91", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "sheffie.bonnick", "Sheffie", "436", 0, "Bonnick" },
                    { 85, 0, "70", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "dominick.evennett", "Dominick", "721", 0, "Evennett" },
                    { 86, 0, "74", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "peirce.kembley", "Peirce", "273", 0, "Kembley" },
                    { 87, 0, "74", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "torrance.gianettini", "Torrance", "539", 0, "Gianettini" },
                    { 88, 0, "68", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "bryce.sutherden", "Bryce", "373", 0, "Sutherden" },
                    { 89, 0, "71", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "alexandra.mckeney", "Alexandra", "914", 0, "McKeney" },
                    { 90, 0, "82", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "paul.grieger", "Paul", "746", 0, "Grieger" },
                    { 91, 0, "11", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "prudi.torns", "Prudi", "689", 0, "Torns" },
                    { 92, 0, "75", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "madonna.costelow", "Madonna", "426", 0, "Costelow" },
                    { 93, 0, "96", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "brandie.luparti", "Brandie", "842", 0, "Luparti" },
                    { 94, 0, "89", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ky.epelett", "Ky", "318", 0, "Epelett" },
                    { 95, 0, "44", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "hester.peracco", "Hester", "374", 0, "Peracco" },
                    { 96, 0, "60", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "gill.starbucke", "Gill", "196", 0, "Starbucke" },
                    { 97, 0, "23", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "jerrie.stoter", "Jerrie", "868", 0, "Stoter" },
                    { 98, 0, "13", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "urbanus.fancet", "Urbanus", "733", 0, "Fancet" },
                    { 99, 0, "74", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "michaelina.mcbeith", "Michaelina", "897", 0, "McBeith" },
                    { 100, 0, "41", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "fedora.sonier", "Fedora", "621", 0, "Sonier" },
                    { 101, 0, "61", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "berenice.sewter", "Berenice", "797", 0, "Sewter" }
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
