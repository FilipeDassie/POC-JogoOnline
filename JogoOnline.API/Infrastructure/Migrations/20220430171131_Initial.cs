using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JogoOnline.API.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "JogoOnline");

            migrationBuilder.CreateTable(
                name: "Game",
                schema: "JogoOnline",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(nullable: true),
                    Version = table.Column<string>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", nullable: false),
                    Description = table.Column<string>(type: "varchar(max)", nullable: false),
                    Year = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Game", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Player",
                schema: "JogoOnline",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(nullable: true),
                    Version = table.Column<string>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", nullable: false),
                    Email = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GameResult",
                schema: "JogoOnline",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(nullable: true),
                    Version = table.Column<string>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    PlayerId = table.Column<string>(nullable: false),
                    GameId = table.Column<string>(nullable: false),
                    Win = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameResult", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameResult_Game_GameId",
                        column: x => x.GameId,
                        principalSchema: "JogoOnline",
                        principalTable: "Game",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GameResult_Player_PlayerId",
                        column: x => x.PlayerId,
                        principalSchema: "JogoOnline",
                        principalTable: "Player",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameResult_GameId",
                schema: "JogoOnline",
                table: "GameResult",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_GameResult_PlayerId",
                schema: "JogoOnline",
                table: "GameResult",
                column: "PlayerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameResult",
                schema: "JogoOnline");

            migrationBuilder.DropTable(
                name: "Game",
                schema: "JogoOnline");

            migrationBuilder.DropTable(
                name: "Player",
                schema: "JogoOnline");
        }
    }
}
