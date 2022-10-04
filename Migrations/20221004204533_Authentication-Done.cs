using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackAllBackend.Migrations
{
    public partial class AuthenticationDone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MarketPlaceMaps",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RestaurantId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Zomato = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Swiggy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UberEats = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FoodPanda = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketPlaceMaps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MarketPlaceMaps_AspNetUsers_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MarketPlaceMaps_RestaurantId",
                table: "MarketPlaceMaps",
                column: "RestaurantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MarketPlaceMaps");
        }
    }
}
