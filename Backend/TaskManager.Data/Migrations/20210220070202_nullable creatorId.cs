using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskManager.Data.Migrations
{
    public partial class nullablecreatorId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskFolders_AspNetUsers_CreatorId",
                table: "TaskFolders");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Tasks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 2, 20, 10, 2, 2, 106, DateTimeKind.Local).AddTicks(7044),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 2, 15, 23, 39, 40, 900, DateTimeKind.Local).AddTicks(4673));

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatorId",
                table: "TaskFolders",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskFolders_AspNetUsers_CreatorId",
                table: "TaskFolders",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskFolders_AspNetUsers_CreatorId",
                table: "TaskFolders");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Tasks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 2, 15, 23, 39, 40, 900, DateTimeKind.Local).AddTicks(4673),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 2, 20, 10, 2, 2, 106, DateTimeKind.Local).AddTicks(7044));

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatorId",
                table: "TaskFolders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskFolders_AspNetUsers_CreatorId",
                table: "TaskFolders",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
