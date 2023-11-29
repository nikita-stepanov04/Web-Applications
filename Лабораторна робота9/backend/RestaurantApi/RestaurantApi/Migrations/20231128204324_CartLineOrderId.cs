using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantApi.Migrations
{
    /// <inheritdoc />
    public partial class CartLineOrderId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartLines_Orders_OrderId",
                table: "CartLines");

            migrationBuilder.AlterColumn<long>(
                name: "OrderId",
                table: "CartLines",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CartLines_Orders_OrderId",
                table: "CartLines",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartLines_Orders_OrderId",
                table: "CartLines");

            migrationBuilder.AlterColumn<long>(
                name: "OrderId",
                table: "CartLines",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_CartLines_Orders_OrderId",
                table: "CartLines",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }
    }
}
