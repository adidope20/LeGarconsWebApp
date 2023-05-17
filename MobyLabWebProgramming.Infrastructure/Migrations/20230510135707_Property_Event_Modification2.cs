using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MobyLabWebProgramming.Infrastructure.Migrations
{
    public partial class Property_Event_Modification2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Event_Reservation_ReservationId",
                table: "Event");

            migrationBuilder.DropIndex(
                name: "IX_Event_ReservationId",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "ReservationId",
                table: "Event");

            migrationBuilder.AddColumn<Guid>(
                name: "EventId",
                table: "Reservation",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_EventId",
                table: "Reservation",
                column: "EventId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Event_EventId",
                table: "Reservation",
                column: "EventId",
                principalTable: "Event",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Event_EventId",
                table: "Reservation");

            migrationBuilder.DropIndex(
                name: "IX_Reservation_EventId",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "Reservation");

            migrationBuilder.AddColumn<Guid>(
                name: "ReservationId",
                table: "Event",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Event_ReservationId",
                table: "Event",
                column: "ReservationId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Event_Reservation_ReservationId",
                table: "Event",
                column: "ReservationId",
                principalTable: "Reservation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
