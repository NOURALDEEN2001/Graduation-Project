using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Huddle.Domain.Migrations
{
    /// <inheritdoc />
    public partial class addingFollowRelations1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FollowedBusinessOwner_Consumers_ConsumerId",
                table: "FollowedBusinessOwner");

            migrationBuilder.DropForeignKey(
                name: "FK_FollowedBusinessOwner_businessOwners_BusinessOwnerId",
                table: "FollowedBusinessOwner");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FollowedBusinessOwner",
                table: "FollowedBusinessOwner");

            migrationBuilder.RenameTable(
                name: "FollowedBusinessOwner",
                newName: "FollowedBusinessOwners");

            migrationBuilder.RenameIndex(
                name: "IX_FollowedBusinessOwner_ConsumerId",
                table: "FollowedBusinessOwners",
                newName: "IX_FollowedBusinessOwners_ConsumerId");

            migrationBuilder.RenameIndex(
                name: "IX_FollowedBusinessOwner_BusinessOwnerId",
                table: "FollowedBusinessOwners",
                newName: "IX_FollowedBusinessOwners_BusinessOwnerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FollowedBusinessOwners",
                table: "FollowedBusinessOwners",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FollowedBusinessOwners_Consumers_ConsumerId",
                table: "FollowedBusinessOwners",
                column: "ConsumerId",
                principalTable: "Consumers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FollowedBusinessOwners_businessOwners_BusinessOwnerId",
                table: "FollowedBusinessOwners",
                column: "BusinessOwnerId",
                principalTable: "businessOwners",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FollowedBusinessOwners_Consumers_ConsumerId",
                table: "FollowedBusinessOwners");

            migrationBuilder.DropForeignKey(
                name: "FK_FollowedBusinessOwners_businessOwners_BusinessOwnerId",
                table: "FollowedBusinessOwners");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FollowedBusinessOwners",
                table: "FollowedBusinessOwners");

            migrationBuilder.RenameTable(
                name: "FollowedBusinessOwners",
                newName: "FollowedBusinessOwner");

            migrationBuilder.RenameIndex(
                name: "IX_FollowedBusinessOwners_ConsumerId",
                table: "FollowedBusinessOwner",
                newName: "IX_FollowedBusinessOwner_ConsumerId");

            migrationBuilder.RenameIndex(
                name: "IX_FollowedBusinessOwners_BusinessOwnerId",
                table: "FollowedBusinessOwner",
                newName: "IX_FollowedBusinessOwner_BusinessOwnerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FollowedBusinessOwner",
                table: "FollowedBusinessOwner",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FollowedBusinessOwner_Consumers_ConsumerId",
                table: "FollowedBusinessOwner",
                column: "ConsumerId",
                principalTable: "Consumers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FollowedBusinessOwner_businessOwners_BusinessOwnerId",
                table: "FollowedBusinessOwner",
                column: "BusinessOwnerId",
                principalTable: "businessOwners",
                principalColumn: "Id");
        }
    }
}
