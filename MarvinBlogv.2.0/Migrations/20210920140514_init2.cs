using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MarvinBlogv._2._0.Migrations
{
    public partial class init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Posts",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(70) CHARACTER SET utf8mb4",
                oldMaxLength: 70);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Posts",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(70) CHARACTER SET utf8mb4",
                oldMaxLength: 70);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2021, 9, 20, 15, 5, 13, 182, DateTimeKind.Local).AddTicks(1256));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2021, 9, 20, 15, 5, 13, 182, DateTimeKind.Local).AddTicks(863));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2021, 9, 20, 15, 5, 13, 182, DateTimeKind.Local).AddTicks(1207));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2021, 9, 20, 15, 5, 13, 182, DateTimeKind.Local).AddTicks(2903));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2021, 9, 20, 15, 5, 13, 179, DateTimeKind.Local).AddTicks(2442));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Posts",
                type: "varchar(70) CHARACTER SET utf8mb4",
                maxLength: 70,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Posts",
                type: "varchar(70) CHARACTER SET utf8mb4",
                maxLength: 70,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2021, 9, 20, 14, 49, 51, 305, DateTimeKind.Local).AddTicks(956));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2021, 9, 20, 14, 49, 51, 305, DateTimeKind.Local).AddTicks(203));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2021, 9, 20, 14, 49, 51, 305, DateTimeKind.Local).AddTicks(867));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2021, 9, 20, 14, 49, 51, 305, DateTimeKind.Local).AddTicks(8254));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2021, 9, 20, 14, 49, 51, 300, DateTimeKind.Local).AddTicks(1222));
        }
    }
}
