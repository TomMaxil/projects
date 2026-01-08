using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eproject.Migrations
{
    /// <inheritdoc />
    public partial class addmigrationSuccessfullyAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_brand",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Brand_Name = table.Column<string>(type: "Varchar(30)", maxLength: 100, nullable: false),
                    Brand_Image = table.Column<string>(type: "Varchar(255)", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_brand", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_category",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Category_Name = table.Column<string>(type: "Varchar(20)", maxLength: 20, nullable: false),
                    Category_Desc = table.Column<string>(type: "Varchar(220)", maxLength: 220, nullable: false),
                    Category_Image = table.Column<string>(type: "Varchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_category", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_customer",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Customer_FirstName = table.Column<string>(type: "Varchar(20)", maxLength: 20, nullable: false),
                    Customer_LastName = table.Column<string>(type: "Varchar(30)", maxLength: 530, nullable: false),
                    Customer_Email = table.Column<string>(type: "Varchar(50)", maxLength: 50, nullable: false),
                    Customer_Password = table.Column<string>(type: "Varchar(100)", nullable: false),
                    Customer_Street = table.Column<string>(type: "Varchar(50)", maxLength: 50, nullable: false),
                    Customer_City = table.Column<string>(type: "Varchar(30)", maxLength: 30, nullable: false),
                    Customer_State = table.Column<string>(type: "Varchar(10)", maxLength: 10, nullable: false),
                    Customer_Country = table.Column<string>(type: "Varchar(20)", maxLength: 20, nullable: false),
                    Customer_Mobile = table.Column<string>(type: "Varchar(15)", maxLength: 15, nullable: false),
                    Customer_Creditcardnumber = table.Column<string>(type: "Varchar(20)", maxLength: 20, nullable: false),
                    Customer_Creaditcardexpiry = table.Column<string>(type: "Varchar(20)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_customer", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_products",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    prod_name = table.Column<string>(type: "Varchar(20)", maxLength: 200, nullable: false),
                    prod_description = table.Column<string>(type: "Varchar(220)", maxLength: 500, nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    prod_Unicost = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    prod_Discount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    prod_image = table.Column<string>(type: "Varchar(255)", maxLength: 300, nullable: true),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BrandId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_products", x => x.id);
                    table.ForeignKey(
                        name: "FK_tbl_products_tbl_brand_BrandId",
                        column: x => x.BrandId,
                        principalTable: "tbl_brand",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_products_tbl_category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "tbl_category",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_cart",
                columns: table => new
                {
                    CartId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_cart", x => x.CartId);
                    table.ForeignKey(
                        name: "FK_tbl_cart_tbl_customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "tbl_customer",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_order",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateOfPurchase = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Order_totalamount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Order_paymentstatus = table.Column<string>(type: "Varchar(255)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_order", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_tbl_order_tbl_customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "tbl_customer",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_productreview",
                columns: table => new
                {
                    ReviewId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Pr_rating = table.Column<int>(type: "int", nullable: false),
                    Pr_comment = table.Column<string>(type: "Varchar(220)", maxLength: 500, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_productreview", x => x.ReviewId);
                    table.ForeignKey(
                        name: "FK_tbl_productreview_tbl_customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "tbl_customer",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_productreview_tbl_products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "tbl_products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_cartitem",
                columns: table => new
                {
                    CartItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CartId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Cartitem_quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_cartitem", x => x.CartItemId);
                    table.ForeignKey(
                        name: "FK_tbl_cartitem_tbl_cart_CartId",
                        column: x => x.CartId,
                        principalTable: "tbl_cart",
                        principalColumn: "CartId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_cartitem_tbl_products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "tbl_products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_orderitem",
                columns: table => new
                {
                    OrderItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Orderitem_quantity = table.Column<int>(type: "int", nullable: false),
                    Orderitem_unicost = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Orderitem_total = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_orderitem", x => x.OrderItemId);
                    table.ForeignKey(
                        name: "FK_tbl_orderitem_tbl_order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "tbl_order",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_orderitem_tbl_products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "tbl_products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_cart_CustomerId",
                table: "tbl_cart",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_cartitem_CartId",
                table: "tbl_cartitem",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_cartitem_ProductId",
                table: "tbl_cartitem",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_order_CustomerId",
                table: "tbl_order",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_orderitem_OrderId",
                table: "tbl_orderitem",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_orderitem_ProductId",
                table: "tbl_orderitem",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_productreview_CustomerId",
                table: "tbl_productreview",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_productreview_ProductId",
                table: "tbl_productreview",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_products_BrandId",
                table: "tbl_products",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_products_CategoryId",
                table: "tbl_products",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_cartitem");

            migrationBuilder.DropTable(
                name: "tbl_orderitem");

            migrationBuilder.DropTable(
                name: "tbl_productreview");

            migrationBuilder.DropTable(
                name: "tbl_cart");

            migrationBuilder.DropTable(
                name: "tbl_order");

            migrationBuilder.DropTable(
                name: "tbl_products");

            migrationBuilder.DropTable(
                name: "tbl_customer");

            migrationBuilder.DropTable(
                name: "tbl_brand");

            migrationBuilder.DropTable(
                name: "tbl_category");
        }
    }
}
