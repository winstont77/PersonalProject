﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BetServer.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AwayTeamOdds = table.Column<double>(type: "float", nullable: false),
                    AwayTeamName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AwayTeamScore = table.Column<int>(type: "int", nullable: false),
                    AwayTeamPoint = table.Column<double>(type: "float", nullable: false),
                    HomeTeamOdds = table.Column<double>(type: "float", nullable: false),
                    HomeTeamName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HomeTeamScore = table.Column<int>(type: "int", nullable: false),
                    HomeTeamPoint = table.Column<double>(type: "float", nullable: false),
                    Sports = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CloseEvent = table.Column<bool>(type: "bit", nullable: false),
                    WinTeamName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LoseTeamName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AwayTeamMoney = table.Column<int>(type: "int", nullable: false),
                    HomeTeamMoney = table.Column<int>(type: "int", nullable: false),
                    PointStatus = table.Column<bool>(type: "bit", nullable: false),
                    Point = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Money = table.Column<int>(type: "int", nullable: true),
                    Profit = table.Column<int>(type: "int", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventId = table.Column<int>(type: "int", nullable: false),
                    DateTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Money = table.Column<int>(type: "int", nullable: false),
                    AwayTeamName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HomeTeamName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AwayTeamOdds = table.Column<float>(type: "real", nullable: false),
                    HomeTeamOdds = table.Column<float>(type: "real", nullable: false),
                    Sports = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BetTeamName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BetTeamOdds = table.Column<float>(type: "real", nullable: false),
                    BetStatus = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CloseEvent = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bets_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bets_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bets_EventId",
                table: "Bets",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Bets_UserId",
                table: "Bets",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bets");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
