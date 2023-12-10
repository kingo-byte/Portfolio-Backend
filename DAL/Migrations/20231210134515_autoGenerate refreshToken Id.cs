using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portfolio_Backend.Migrations
{
    public partial class autoGeneraterefreshTokenId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RefreshTokenId",
                table: "RefreshTokens",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "RefreshTokens",
                newName: "RefreshTokenId");
        }
    }
}
