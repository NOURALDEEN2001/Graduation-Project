using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Huddle.Domain.Migrations
{
    /// <inheritdoc />
    public partial class addingConsumerActivityTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConsumerActivity_Activity_ActivityId",
                table: "ConsumerActivity");

            migrationBuilder.DropForeignKey(
                name: "FK_ConsumerActivity_Consumers_ConsumerId",
                table: "ConsumerActivity");

            migrationBuilder.RenameTable(
                name: "ConsumerActivity",
                newName: "ConsumerActivities");

            migrationBuilder.RenameIndex(
                name: "IX_ConsumerActivity_ConsumerId",
                table: "ConsumerActivities",
                newName: "IX_ConsumerActivities_ConsumerId");

            migrationBuilder.RenameIndex(
                name: "IX_ConsumerActivity_ActivityId",
                table: "ConsumerActivities",
                newName: "IX_ConsumerActivities_ActivityId");

            migrationBuilder.AddForeignKey(
                name: "FK_ConsumerActivities_Activity_ActivityId",
                table: "ConsumerActivities",
                column: "ActivityId",
                principalTable: "Activity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConsumerActivities_Consumers_ConsumerId",
                table: "ConsumerActivities",
                column: "ConsumerId",
                principalTable: "Consumers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConsumerActivities_Activity_ActivityId",
                table: "ConsumerActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_ConsumerActivities_Consumers_ConsumerId",
                table: "ConsumerActivities");

            migrationBuilder.RenameTable(
                name: "ConsumerActivities",
                newName: "ConsumerActivity");

            migrationBuilder.RenameIndex(
                name: "IX_ConsumerActivities_ConsumerId",
                table: "ConsumerActivity",
                newName: "IX_ConsumerActivity_ConsumerId");

            migrationBuilder.RenameIndex(
                name: "IX_ConsumerActivities_ActivityId",
                table: "ConsumerActivity",
                newName: "IX_ConsumerActivity_ActivityId");

            migrationBuilder.AddForeignKey(
                name: "FK_ConsumerActivity_Activity_ActivityId",
                table: "ConsumerActivity",
                column: "ActivityId",
                principalTable: "Activity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConsumerActivity_Consumers_ConsumerId",
                table: "ConsumerActivity",
                column: "ConsumerId",
                principalTable: "Consumers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
