using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Huddle.Domain.Migrations
{
    /// <inheritdoc />
    public partial class ReAddingKeyForActivePlaceInGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ActivePlacesInGroups_GroupId",
                table: "ActivePlacesInGroups");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ActivePlacesInGroups");

            migrationBuilder.AlterColumn<string>(
                name: "PlaceId",
                table: "ActivePlacesInGroups",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ActivePlacesInGroups",
                table: "ActivePlacesInGroups",
                columns: new[] { "GroupId", "PlaceId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ActivePlacesInGroups",
                table: "ActivePlacesInGroups");

            migrationBuilder.AlterColumn<string>(
                name: "PlaceId",
                table: "ActivePlacesInGroups",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ActivePlacesInGroups",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ActivePlacesInGroups_GroupId",
                table: "ActivePlacesInGroups",
                column: "GroupId");
        }
    }
}
