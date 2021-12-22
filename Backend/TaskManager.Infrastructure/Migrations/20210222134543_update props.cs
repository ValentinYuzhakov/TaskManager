using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskManager.Data.Migrations
{
    public partial class updateprops : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TaskStatus",
                table: "Tasks",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "TaskPriority",
                table: "Tasks",
                newName: "Priority");

            migrationBuilder.RenameColumn(
                name: "FolderType",
                table: "TaskFolders",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "TaskStatus",
                table: "SubTasks",
                newName: "Status");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Tasks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 2, 22, 16, 45, 43, 16, DateTimeKind.Local).AddTicks(7295),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 2, 21, 22, 55, 10, 402, DateTimeKind.Local).AddTicks(7196));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Tasks",
                newName: "TaskStatus");

            migrationBuilder.RenameColumn(
                name: "Priority",
                table: "Tasks",
                newName: "TaskPriority");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "TaskFolders",
                newName: "FolderType");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "SubTasks",
                newName: "TaskStatus");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Tasks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 2, 21, 22, 55, 10, 402, DateTimeKind.Local).AddTicks(7196),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 2, 22, 16, 45, 43, 16, DateTimeKind.Local).AddTicks(7295));
        }
    }
}
