using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RPSSL.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Scores",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PlayerOne = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    PlayerOneChoice = table.Column<int>(type: "integer", nullable: false),
                    PlayerTwo = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    PlayerTwoChoice = table.Column<int>(type: "integer", nullable: false),
                    Result = table.Column<int>(type: "integer", nullable: false),
                    PlayDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scores", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Scores");
        }
    }
}
