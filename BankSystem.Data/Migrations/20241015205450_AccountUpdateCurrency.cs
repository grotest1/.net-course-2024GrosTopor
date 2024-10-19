using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class AccountUpdateCurrency : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "currency_name",
                table: "accounts");

            migrationBuilder.AddColumn<Guid>(
                name: "currency_id",
                table: "accounts",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "currancies",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    code = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_currancies", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_accounts_currency_id",
                table: "accounts",
                column: "currency_id");

            migrationBuilder.AddForeignKey(
                name: "FK_accounts_currancies_currency_id",
                table: "accounts",
                column: "currency_id",
                principalTable: "currancies",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_accounts_currancies_currency_id",
                table: "accounts");

            migrationBuilder.DropTable(
                name: "currancies");

            migrationBuilder.DropIndex(
                name: "IX_accounts_currency_id",
                table: "accounts");

            migrationBuilder.DropColumn(
                name: "currency_id",
                table: "accounts");

            migrationBuilder.AddColumn<string>(
                name: "currency_name",
                table: "accounts",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
