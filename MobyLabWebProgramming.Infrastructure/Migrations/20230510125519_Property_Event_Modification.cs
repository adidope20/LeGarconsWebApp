using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MobyLabWebProgramming.Infrastructure.Migrations
{
    public partial class Property_Event_Modification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PropertyFile");

            migrationBuilder.AddColumn<Guid>(
                name: "PropertyId",
                table: "Event",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Event_PropertyId",
                table: "Event",
                column: "PropertyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Event_Property_PropertyId",
                table: "Event",
                column: "PropertyId",
                principalTable: "Property",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Event_Property_PropertyId",
                table: "Event");

            migrationBuilder.DropIndex(
                name: "IX_Event_PropertyId",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "PropertyId",
                table: "Event");

            migrationBuilder.CreateTable(
                name: "PropertyFile",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PropertyId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Description = table.Column<string>(type: "character varying(4095)", maxLength: 4095, nullable: true),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Path = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyFile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertyFile_Property_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Property",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PropertyFile_PropertyId",
                table: "PropertyFile",
                column: "PropertyId");
        }
    }
}
