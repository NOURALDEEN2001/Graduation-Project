using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Huddle.Domain.Migrations
{
    /// <inheritdoc />
    public partial class addingFollowRelations2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FollowedEventPlanners",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConsumerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventPlannerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FollowedEventPlanners", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FollowedEventPlanners_Consumers_ConsumerId",
                        column: x => x.ConsumerId,
                        principalTable: "Consumers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FollowedEventPlanners_EventPlanners_EventPlannerId",
                        column: x => x.EventPlannerId,
                        principalTable: "EventPlanners",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FollowedEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConsumerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FollowedEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FollowedEvents_Consumers_ConsumerId",
                        column: x => x.ConsumerId,
                        principalTable: "Consumers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FollowedEvents_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_FollowedEventPlanners_ConsumerId",
                table: "FollowedEventPlanners",
                column: "ConsumerId");

            migrationBuilder.CreateIndex(
                name: "IX_FollowedEventPlanners_EventPlannerId",
                table: "FollowedEventPlanners",
                column: "EventPlannerId");

            migrationBuilder.CreateIndex(
                name: "IX_FollowedEvents_ConsumerId",
                table: "FollowedEvents",
                column: "ConsumerId");

            migrationBuilder.CreateIndex(
                name: "IX_FollowedEvents_EventId",
                table: "FollowedEvents",
                column: "EventId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FollowedEventPlanners");

            migrationBuilder.DropTable(
                name: "FollowedEvents");
        }
    }
}
