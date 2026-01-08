using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjInventroy.Migrations
{
    /// <inheritdoc />
    public partial class InitialsCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_category",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cat_name = table.Column<string>(type: "Varchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_category", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "tbl_sale",
                columns: table => new
                {
                    SaleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sale_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    sale_amount = table.Column<decimal>(type: "Decimal(18,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_sale", x => x.SaleId);
                });

            migrationBuilder.CreateTable(
                name: "tbl_supplies",
                columns: table => new
                {
                    SupplierId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sup_name = table.Column<string>(type: "Varchar(20)", maxLength: 20, nullable: false),
                    sup_phone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_supplies", x => x.SupplierId);
                });

            migrationBuilder.CreateTable(
                name: "tbl_product",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    prod_name = table.Column<string>(type: "Varchar(20)", maxLength: 20, nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    prod_price = table.Column<decimal>(type: "Decimal(30,0)", nullable: false),
                    prod_Qty = table.Column<int>(type: "int", nullable: false),
                    prod_MinQty = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_product", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_tbl_product_tbl_category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "tbl_category",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_puchase",
                columns: table => new
                {
                    PurchaseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupplierId = table.Column<int>(type: "int", nullable: false),
                    pur_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    pur_Amount = table.Column<decimal>(type: "Decimal(18,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_puchase", x => x.PurchaseId);
                    table.ForeignKey(
                        name: "FK_tbl_puchase_tbl_supplies_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "tbl_supplies",
                        principalColumn: "SupplierId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_saleitem",
                columns: table => new
                {
                    SaleItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SaleId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    saleitem_Qty = table.Column<int>(type: "int", nullable: false),
                    saleitem_price = table.Column<decimal>(type: "Decimal(18,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_saleitem", x => x.SaleItemId);
                    table.ForeignKey(
                        name: "FK_tbl_saleitem_tbl_product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "tbl_product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_saleitem_tbl_sale_SaleId",
                        column: x => x.SaleId,
                        principalTable: "tbl_sale",
                        principalColumn: "SaleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_puchaseitem",
                columns: table => new
                {
                    PurchaseItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PurchaseId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    purI_qty = table.Column<int>(type: "int", nullable: false),
                    purI_costprice = table.Column<decimal>(type: "Decimal(18,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_puchaseitem", x => x.PurchaseItemId);
                    table.ForeignKey(
                        name: "FK_tbl_puchaseitem_tbl_product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "tbl_product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_puchaseitem_tbl_puchase_PurchaseId",
                        column: x => x.PurchaseId,
                        principalTable: "tbl_puchase",
                        principalColumn: "PurchaseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_product_CategoryId",
                table: "tbl_product",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_puchase_SupplierId",
                table: "tbl_puchase",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_puchaseitem_ProductId",
                table: "tbl_puchaseitem",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_puchaseitem_PurchaseId",
                table: "tbl_puchaseitem",
                column: "PurchaseId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_saleitem_ProductId",
                table: "tbl_saleitem",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_saleitem_SaleId",
                table: "tbl_saleitem",
                column: "SaleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_puchaseitem");

            migrationBuilder.DropTable(
                name: "tbl_saleitem");

            migrationBuilder.DropTable(
                name: "tbl_puchase");

            migrationBuilder.DropTable(
                name: "tbl_product");

            migrationBuilder.DropTable(
                name: "tbl_sale");

            migrationBuilder.DropTable(
                name: "tbl_supplies");

            migrationBuilder.DropTable(
                name: "tbl_category");
        }
    }
}
