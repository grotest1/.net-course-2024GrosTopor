using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Salary",
                table: "employees",
                newName: "salary");

            migrationBuilder.RenameColumn(
                name: "Contract",
                table: "employees",
                newName: "contract");

            migrationBuilder.AlterColumn<string>(
                name: "contract",
                table: "employees",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "salary",
                table: "employees",
                newName: "Salary");

            migrationBuilder.RenameColumn(
                name: "contract",
                table: "employees",
                newName: "Contract");

            migrationBuilder.AlterColumn<string>(
                name: "Contract",
                table: "employees",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(1000)",
                oldMaxLength: 1000);
        }
    }
}
