using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Huddle.Domain.Migrations
{
    /// <inheritdoc />
    public partial class fixMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "EventId",
                table: "Reviews",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "BusinessOwnerId",
                table: "Reviews",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateTable(
                name: "FollowedBusinessOwner",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BusinessOwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConsumerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FollowedBusinessOwner", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FollowedBusinessOwner_Consumers_ConsumerId",
                        column: x => x.ConsumerId,
                        principalTable: "Consumers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FollowedBusinessOwner_businessOwners_BusinessOwnerId",
                        column: x => x.BusinessOwnerId,
                        principalTable: "businessOwners",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_FollowedBusinessOwner_BusinessOwnerId",
                table: "FollowedBusinessOwner",
                column: "BusinessOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_FollowedBusinessOwner_ConsumerId",
                table: "FollowedBusinessOwner",
                column: "ConsumerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FollowedBusinessOwner");

            migrationBuilder.AlterColumn<Guid>(
                name: "EventId",
                table: "Reviews",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "BusinessOwnerId",
                table: "Reviews",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);
        }
    }
}
