using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskManager.Data.Migrations
{
    public partial class updatetokenmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "RevokeDate",
                table: "RefreshToken",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RevokeDate",
                table: "RefreshToken");
        }
    }
}
