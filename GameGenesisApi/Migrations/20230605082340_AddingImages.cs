using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameGenesisApi.Migrations
{
    /// <inheritdoc />
    public partial class AddingImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Users_UserId",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_BasketProduct_Baskets_BasketsId",
                table: "BasketProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_BasketProduct_Products_ProductsId",
                table: "BasketProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_Baskets_Accounts_AccountId",
                table: "Baskets");

            migrationBuilder.DropForeignKey(
                name: "FK_LibraryProduct_Librarys_LibrariesId",
                table: "LibraryProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_LibraryProduct_Products_ProductsId",
                table: "LibraryProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_Librarys_Accounts_AccountId",
                table: "Librarys");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductProductCategory_ProductCategorys_ProductCategoriesId",
                table: "ProductProductCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductProductCategory_Products_ProductsId",
                table: "ProductProductCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Users_AuthorId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductShop_Products_ProductsId",
                table: "ProductShop");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductShop_Shops_ShopsId",
                table: "ProductShop");

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Images_ProductId",
                table: "Images",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Users_UserId",
                table: "Accounts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_BasketProduct_Baskets_BasketsId",
                table: "BasketProduct",
                column: "BasketsId",
                principalTable: "Baskets",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_BasketProduct_Products_ProductsId",
                table: "BasketProduct",
                column: "ProductsId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Baskets_Accounts_AccountId",
                table: "Baskets",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_LibraryProduct_Librarys_LibrariesId",
                table: "LibraryProduct",
                column: "LibrariesId",
                principalTable: "Librarys",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_LibraryProduct_Products_ProductsId",
                table: "LibraryProduct",
                column: "ProductsId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Librarys_Accounts_AccountId",
                table: "Librarys",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductProductCategory_ProductCategorys_ProductCategoriesId",
                table: "ProductProductCategory",
                column: "ProductCategoriesId",
                principalTable: "ProductCategorys",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductProductCategory_Products_ProductsId",
                table: "ProductProductCategory",
                column: "ProductsId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Users_AuthorId",
                table: "Products",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductShop_Products_ProductsId",
                table: "ProductShop",
                column: "ProductsId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductShop_Shops_ShopsId",
                table: "ProductShop",
                column: "ShopsId",
                principalTable: "Shops",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Users_UserId",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_BasketProduct_Baskets_BasketsId",
                table: "BasketProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_BasketProduct_Products_ProductsId",
                table: "BasketProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_Baskets_Accounts_AccountId",
                table: "Baskets");

            migrationBuilder.DropForeignKey(
                name: "FK_LibraryProduct_Librarys_LibrariesId",
                table: "LibraryProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_LibraryProduct_Products_ProductsId",
                table: "LibraryProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_Librarys_Accounts_AccountId",
                table: "Librarys");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductProductCategory_ProductCategorys_ProductCategoriesId",
                table: "ProductProductCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductProductCategory_Products_ProductsId",
                table: "ProductProductCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Users_AuthorId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductShop_Products_ProductsId",
                table: "ProductShop");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductShop_Shops_ShopsId",
                table: "ProductShop");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Users_UserId",
                table: "Accounts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BasketProduct_Baskets_BasketsId",
                table: "BasketProduct",
                column: "BasketsId",
                principalTable: "Baskets",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BasketProduct_Products_ProductsId",
                table: "BasketProduct",
                column: "ProductsId",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Baskets_Accounts_AccountId",
                table: "Baskets",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LibraryProduct_Librarys_LibrariesId",
                table: "LibraryProduct",
                column: "LibrariesId",
                principalTable: "Librarys",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LibraryProduct_Products_ProductsId",
                table: "LibraryProduct",
                column: "ProductsId",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Librarys_Accounts_AccountId",
                table: "Librarys",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductProductCategory_ProductCategorys_ProductCategoriesId",
                table: "ProductProductCategory",
                column: "ProductCategoriesId",
                principalTable: "ProductCategorys",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductProductCategory_Products_ProductsId",
                table: "ProductProductCategory",
                column: "ProductsId",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Users_AuthorId",
                table: "Products",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductShop_Products_ProductsId",
                table: "ProductShop",
                column: "ProductsId",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductShop_Shops_ShopsId",
                table: "ProductShop",
                column: "ShopsId",
                principalTable: "Shops",
                principalColumn: "Id");
        }
    }
}
