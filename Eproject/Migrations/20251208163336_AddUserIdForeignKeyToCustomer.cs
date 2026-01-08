using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eproject.Migrations
{
    /// <inheritdoc />
    public partial class AddUserIdForeignKeyToCustomer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "tbl_customer",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_tbl_customer_UserId",
                table: "tbl_customer",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_customer_users_UserId",
                table: "tbl_customer",
                column: "UserId",
                principalTable: "users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_customer_users_UserId",
                table: "tbl_customer");

            migrationBuilder.DropIndex(
                name: "IX_tbl_customer_UserId",
                table: "tbl_customer");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "tbl_customer");
        }
    }
}
