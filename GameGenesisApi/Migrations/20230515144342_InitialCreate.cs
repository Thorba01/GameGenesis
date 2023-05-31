using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GameGenesisApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductCategorys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategorys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Shops",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shops", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailVerified = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Birthdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accounts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuthorId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Users_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Baskets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Baskets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Baskets_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Librarys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Librarys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Librarys_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "ProductProductCategory",
                columns: table => new
                {
                    ProductCategoriesId = table.Column<int>(type: "int", nullable: false),
                    ProductsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductProductCategory", x => new { x.ProductCategoriesId, x.ProductsId });
                    table.ForeignKey(
                        name: "FK_ProductProductCategory_ProductCategorys_ProductCategoriesId",
                        column: x => x.ProductCategoriesId,
                        principalTable: "ProductCategorys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ProductProductCategory_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "ProductShop",
                columns: table => new
                {
                    ProductsId = table.Column<int>(type: "int", nullable: false),
                    ShopsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductShop", x => new { x.ProductsId, x.ShopsId });
                    table.ForeignKey(
                        name: "FK_ProductShop_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ProductShop_Shops_ShopsId",
                        column: x => x.ShopsId,
                        principalTable: "Shops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "BasketProduct",
                columns: table => new
                {
                    BasketsId = table.Column<int>(type: "int", nullable: false),
                    ProductsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasketProduct", x => new { x.BasketsId, x.ProductsId });
                    table.ForeignKey(
                        name: "FK_BasketProduct_Baskets_BasketsId",
                        column: x => x.BasketsId,
                        principalTable: "Baskets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_BasketProduct_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "LibraryProduct",
                columns: table => new
                {
                    LibrariesId = table.Column<int>(type: "int", nullable: false),
                    ProductsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LibraryProduct", x => new { x.LibrariesId, x.ProductsId });
                    table.ForeignKey(
                        name: "FK_LibraryProduct_Librarys_LibrariesId",
                        column: x => x.LibrariesId,
                        principalTable: "Librarys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_LibraryProduct_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.InsertData(
                table: "ProductCategorys",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Les jeux d'action mettent l'accent sur les défis physiques, y compris la coordination œil-main et les temps de réaction.", "Action " },
                    { 2, "Ces jeux mettent souvent l'accent sur l'exploration, la résolution de problèmes, l'interaction avec l'environnement et les personnages non joueurs.", "Aventure " },
                    { 3, " Dans ces jeux, les joueurs assument le rôle d'un personnage, souvent dans un cadre fantastique, et prennent des décisions qui affectent le déroulement de l'histoire.", "RPG (Role-Playing Games) " },
                    { 4, "Ces jeux tentent de simuler la réalité. Cela peut aller de la gestion d'une ferme à la conduite d'un avion.", "Simulation " },
                    { 5, "Ces jeux exigent que le joueur pense et planifie stratégiquement pour atteindre la victoire. Ils peuvent être en temps réel ou tour par tour.", "Stratégie " },
                    { 6, "Ces jeux mettent l'accent sur la résolution de problèmes.", "Puzzle " },
                    { 7, "Ces jeux simulent des sports réels comme le football, le basket-ball, etc. ", "Sport " },
                    { 8, " Ces jeux impliquent généralement des courses de véhicules contre des adversaires ou contre le temps.", "Course " },
                    { 9, "Ces jeux mettent l'accent sur la survie du joueur dans un environnement effrayant, généralement avec des ressources limitées.", "Horreur " },
                    { 10, "Dans ces jeux, deux équipes s'affrontent dans une arène. Chaque joueur contrôle un seul personnage avec des compétences uniques. ", "MOBA " },
                    { 11, "Ce sont des RPG joués en ligne avec un grand nombre de personnes. ", "MMORPG" },
                    { 12, " Il s'agit d'un genre de jeu en ligne où un grand nombre de joueurs se bat pour être le dernier survivant.", "Battle Royale " },
                    { 13, "Ces jeux impliquent principalement de naviguer le personnage du joueur à travers divers obstacles.", "Plateforme " },
                    { 14, "Ce sont des jeux généralement caractérisés par des niveaux générés aléatoirement et une mort permanente. ", "Roguelike " },
                    { 15, "Ce sont des jeux d'action vus à la première personne où le joueur utilise des armes à feu et combat des ennemis.", "FPS" },
                    { 16, "Semblables aux FPS, mais vus à la troisième personne, ce qui donne au joueur une vision plus large de l'environnement du jeu.", "TPS " },
                    { 17, "Dans ces jeux, un personnage se bat contre un grand nombre d'ennemis. Ces jeux sont souvent coopératifs.", "Beat 'em up / Brawler" },
                    { 18, "Ces jeux encouragent le joueur à éviter les ennemis plutôt que de les affronter directement.", "Stealth " },
                    { 19, "Ce genre de jeux met l'accent sur la survie. Le joueur commence généralement avec un minimum de ressources et doit collecter des ressources et des objets tout en évitant, ou en confrontant, les menaces.", "Survival " },
                    { 20, "Ce sont des jeux qui impliquent des actions simples et répétitives, comme cliquer sur l'écran, pour gagner des points et progresser dans le jeu. ", "Idle / Clicker / Incremental " },
                    { 21, "Ces jeux mettent l'accent sur la musique et exigent que les joueurs appuient sur des boutons en rythme avec la musique.", "Rythme " },
                    { 22, " Il s'agit d'un genre de jeu narratif, généralement basé sur du texte, où les joueurs lisent une histoire et font parfois des choix qui affectent le déroulement de l'histoire.", "Visual Novel" },
                    { 23, "Ces jeux permettent aux joueurs d'interagir avec l'environnement du jeu de manière créative et sans objectif précis.", "Sandbox " },
                    { 24, " Dans ces jeux, les joueurs placent des tours ou des unités défensives pour arrêter les vagues d'ennemis qui avancent sur un chemin prédéfini.", "Tower Defense" }
                });

            migrationBuilder.InsertData(
                table: "Shops",
                column: "Id",
                value: 1);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_UserId",
                table: "Accounts",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BasketProduct_ProductsId",
                table: "BasketProduct",
                column: "ProductsId");

            migrationBuilder.CreateIndex(
                name: "IX_Baskets_AccountId",
                table: "Baskets",
                column: "AccountId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LibraryProduct_ProductsId",
                table: "LibraryProduct",
                column: "ProductsId");

            migrationBuilder.CreateIndex(
                name: "IX_Librarys_AccountId",
                table: "Librarys",
                column: "AccountId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductProductCategory_ProductsId",
                table: "ProductProductCategory",
                column: "ProductsId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_AuthorId",
                table: "Products",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductShop_ShopsId",
                table: "ProductShop",
                column: "ShopsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BasketProduct");

            migrationBuilder.DropTable(
                name: "LibraryProduct");

            migrationBuilder.DropTable(
                name: "ProductProductCategory");

            migrationBuilder.DropTable(
                name: "ProductShop");

            migrationBuilder.DropTable(
                name: "Baskets");

            migrationBuilder.DropTable(
                name: "Librarys");

            migrationBuilder.DropTable(
                name: "ProductCategorys");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Shops");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
