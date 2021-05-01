using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TicTacToeAPI.Infrastructure.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tictactoegame",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Player1Name = table.Column<string>(type: "TEXT", nullable: true),
                    Player2Name = table.Column<string>(type: "TEXT", nullable: true),
                    Status = table.Column<string>(type: "TEXT", nullable: true),
                    Winner = table.Column<string>(type: "TEXT", nullable: true),
                    GameStartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Cell1 = table.Column<string>(type: "TEXT", nullable: true),
                    Cell2 = table.Column<string>(type: "TEXT", nullable: true),
                    Cell3 = table.Column<string>(type: "TEXT", nullable: true),
                    Cell4 = table.Column<string>(type: "TEXT", nullable: true),
                    Cell5 = table.Column<string>(type: "TEXT", nullable: true),
                    Cell6 = table.Column<string>(type: "TEXT", nullable: true),
                    Cell7 = table.Column<string>(type: "TEXT", nullable: true),
                    Cell8 = table.Column<string>(type: "TEXT", nullable: true),
                    Cell9 = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tictactoegame", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tictactoegame");
        }
    }
}
