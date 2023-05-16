using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Huddle.Domain.Migrations
{
    /// <inheritdoc />
    public partial class EditNameInBusinessOuner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "businessOwners",
                newName: "BusinessName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BusinessName",
                table: "businessOwners",
                newName: "Name");
        }
    }
}
