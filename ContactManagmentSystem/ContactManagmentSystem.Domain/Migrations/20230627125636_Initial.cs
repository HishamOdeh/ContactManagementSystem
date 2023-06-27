using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContactManagementSystem.Domain.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BaseEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contact",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contact_BaseEntity_Id",
                        column: x => x.Id,
                        principalTable: "BaseEntity",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "BaseEntity",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "ModifiedBy", "ModifiedDate" },
                values: new object[] { new Guid("c9895b85-9304-42b7-a5d9-08db6cf9923a"), null, new DateTime(2023, 6, 27, 8, 56, 35, 854, DateTimeKind.Local).AddTicks(9842), null, new DateTime(2023, 6, 27, 8, 56, 35, 854, DateTimeKind.Local).AddTicks(9913) });

            migrationBuilder.InsertData(
                table: "Contact",
                columns: new[] { "Id", "BirthDate", "Email", "FirstName", "LastName", "PhoneNumber" },
                values: new object[] { new Guid("c9895b85-9304-42b7-a5d9-08db6cf9923a"), new DateTime(2001, 11, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "HeshoOdeh@hesho.com", "Hesho", "Odeh", "123-456-7890" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contact");

            migrationBuilder.DropTable(
                name: "BaseEntity");
        }
    }
}
