using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Huddle.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddIdForGroupConsumer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupConsumers",
                table: "GroupConsumers");

            migrationBuilder.DropIndex(
                name: "IX_GroupConsumers_GroupId",
                table: "GroupConsumers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupConsumers",
                table: "GroupConsumers",
                columns: new[] { "GroupId", "ConsumerId" });

            migrationBuilder.CreateIndex(
                name: "IX_GroupConsumers_ConsumerId",
                table: "GroupConsumers",
                column: "ConsumerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupConsumers",
                table: "GroupConsumers");

            migrationBuilder.DropIndex(
                name: "IX_GroupConsumers_ConsumerId",
                table: "GroupConsumers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupConsumers",
                table: "GroupConsumers",
                columns: new[] { "ConsumerId", "GroupId" });

            migrationBuilder.CreateIndex(
                name: "IX_GroupConsumers_GroupId",
                table: "GroupConsumers",
                column: "GroupId");
        }
    }
}
