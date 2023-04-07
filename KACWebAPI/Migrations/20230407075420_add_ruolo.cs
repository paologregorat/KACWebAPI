using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KACWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class addruolo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "RuoloID",
                table: "Utenti",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Ruoli",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Abilitazione = table.Column<int>(type: "integer", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastEditDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ruoli", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Utenti_RuoloID",
                table: "Utenti",
                column: "RuoloID");

            migrationBuilder.AddForeignKey(
                name: "FK_Utenti_Ruoli_RuoloID",
                table: "Utenti",
                column: "RuoloID",
                principalTable: "Ruoli",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Utenti_Ruoli_RuoloID",
                table: "Utenti");

            migrationBuilder.DropTable(
                name: "Ruoli");

            migrationBuilder.DropIndex(
                name: "IX_Utenti_RuoloID",
                table: "Utenti");

            migrationBuilder.DropColumn(
                name: "RuoloID",
                table: "Utenti");
        }
    }
}
