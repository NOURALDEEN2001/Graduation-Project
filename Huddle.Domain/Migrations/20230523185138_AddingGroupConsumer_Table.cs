using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Huddle.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddingGroupConsumer_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupConsumer_Consumers_ConsumerId",
                table: "GroupConsumer");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupConsumer_Groups_GroupId",
                table: "GroupConsumer");

            migrationBuilder.RenameTable(
                name: "GroupConsumer",
                newName: "GroupConsumers");

            migrationBuilder.RenameIndex(
                name: "IX_GroupConsumer_GroupId",
                table: "GroupConsumers",
                newName: "IX_GroupConsumers_GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_GroupConsumer_ConsumerId",
                table: "GroupConsumers",
                newName: "IX_GroupConsumers_ConsumerId");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupConsumers_Consumers_ConsumerId",
                table: "GroupConsumers",
                column: "ConsumerId",
                principalTable: "Consumers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupConsumers_Groups_GroupId",
                table: "GroupConsumers",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupConsumers_Consumers_ConsumerId",
                table: "GroupConsumers");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupConsumers_Groups_GroupId",
                table: "GroupConsumers");

            migrationBuilder.RenameTable(
                name: "GroupConsumers",
                newName: "GroupConsumer");

            migrationBuilder.RenameIndex(
                name: "IX_GroupConsumers_GroupId",
                table: "GroupConsumer",
                newName: "IX_GroupConsumer_GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_GroupConsumers_ConsumerId",
                table: "GroupConsumer",
                newName: "IX_GroupConsumer_ConsumerId");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupConsumer_Consumers_ConsumerId",
                table: "GroupConsumer",
                column: "ConsumerId",
                principalTable: "Consumers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupConsumer_Groups_GroupId",
                table: "GroupConsumer",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
