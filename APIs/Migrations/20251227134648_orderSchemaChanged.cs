using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIs.Migrations
{
    /// <inheritdoc />
    public partial class orderSchemaChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_userId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "Orders",
                newName: "VendorId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_userId",
                table: "Orders",
                newName: "IX_Orders_VendorId");

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Orders",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_CustomerId",
                table: "Orders",
                column: "CustomerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_VendorId",
                table: "Orders",
                column: "VendorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_CustomerId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_VendorId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "VendorId",
                table: "Orders",
                newName: "userId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_VendorId",
                table: "Orders",
                newName: "IX_Orders_userId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_userId",
                table: "Orders",
                column: "userId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
