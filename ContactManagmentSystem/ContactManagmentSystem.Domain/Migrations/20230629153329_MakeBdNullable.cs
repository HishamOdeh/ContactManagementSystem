using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContactManagementSystem.Domain.Migrations
{
    public partial class MakeBdNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "BirthDate",
                table: "Contact",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.UpdateData(
                table: "BaseEntity",
                keyColumn: "Id",
                keyValue: new Guid("c9895b85-9304-42b7-a5d9-08db6cf9923a"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 6, 29, 11, 33, 29, 6, DateTimeKind.Local).AddTicks(5127), new DateTime(2023, 6, 29, 11, 33, 29, 6, DateTimeKind.Local).AddTicks(5158) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "BirthDate",
                table: "Contact",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "BaseEntity",
                keyColumn: "Id",
                keyValue: new Guid("c9895b85-9304-42b7-a5d9-08db6cf9923a"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 6, 27, 8, 56, 35, 854, DateTimeKind.Local).AddTicks(9842), new DateTime(2023, 6, 27, 8, 56, 35, 854, DateTimeKind.Local).AddTicks(9913) });
        }
    }
}
