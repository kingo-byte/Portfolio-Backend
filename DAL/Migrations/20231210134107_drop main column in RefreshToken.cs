using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portfolio_Backend.Migrations
{
    public partial class dropmaincolumninRefreshToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "RefreshTokens",
                newName: "RefreshTokenId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RefreshTokenId",
                table: "RefreshTokens",
                newName: "Id");
        }
    }
}
