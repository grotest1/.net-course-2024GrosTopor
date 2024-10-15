using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "employees",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    Contract = table.Column<string>(type: "text", nullable: false),
                    Salary = table.Column<int>(type: "integer", nullable: false),
                    first_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    last_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    birthday = table.Column<DateOnly>(type: "date", nullable: false),
                    age = table.Column<int>(type: "integer", nullable: false),
                    phone = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    passport = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employees", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "employees");
        }
    }
}
