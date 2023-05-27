using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Huddle.Domain.Migrations
{
    /// <inheritdoc />
    public partial class deleteIdCol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupConsumers",
                table: "GroupConsumers");

            migrationBuilder.DropIndex(
                name: "IX_GroupConsumers_ConsumerId",
                table: "GroupConsumers");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "GroupConsumers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupConsumers",
                table: "GroupConsumers",
                columns: new[] { "ConsumerId", "GroupId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupConsumers",
                table: "GroupConsumers");

            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "GroupConsumers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupConsumers",
                table: "GroupConsumers",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_GroupConsumers_ConsumerId",
                table: "GroupConsumers",
                column: "ConsumerId");
        }
    }
}
