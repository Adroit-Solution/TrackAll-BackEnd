using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackAllBackend.Migrations
{
    public partial class AddedIdentityColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MarketPlaceMaps_AspNetUsers_RestaurantId",
                table: "MarketPlaceMaps");

            migrationBuilder.AlterColumn<string>(
                name: "RestaurantId",
                table: "MarketPlaceMaps",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RestId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                name: "FK_MarketPlaceMaps_AspNetUsers_RestaurantId",
                table: "MarketPlaceMaps",
                column: "RestaurantId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MarketPlaceMaps_AspNetUsers_RestaurantId",
                table: "MarketPlaceMaps");

            migrationBuilder.DropColumn(
                name: "RestId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "RestaurantId",
                table: "MarketPlaceMaps",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_MarketPlaceMaps_AspNetUsers_RestaurantId",
                table: "MarketPlaceMaps",
                column: "RestaurantId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
