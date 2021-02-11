using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskManager.Data.Migrations
{
    public partial class second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ParentFolderId",
                table: "TaskFolders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_TaskFolders_ParentFolderId",
                table: "TaskFolders",
                column: "ParentFolderId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskFolders_TaskFolders_ParentFolderId",
                table: "TaskFolders",
                column: "ParentFolderId",
                principalTable: "TaskFolders",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskFolders_TaskFolders_ParentFolderId",
                table: "TaskFolders");

            migrationBuilder.DropIndex(
                name: "IX_TaskFolders_ParentFolderId",
                table: "TaskFolders");

            migrationBuilder.DropColumn(
                name: "ParentFolderId",
                table: "TaskFolders");
        }
    }
}
