using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MultiShop.Order.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "Orderings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Orderings_AddressId",
                table: "Orderings",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orderings_Addresses_AddressId",
                table: "Orderings",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orderings_Addresses_AddressId",
                table: "Orderings");

            migrationBuilder.DropIndex(
                name: "IX_Orderings_AddressId",
                table: "Orderings");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Orderings");
        }
    }
}
