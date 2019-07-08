using Microsoft.EntityFrameworkCore.Migrations;

namespace ZDLoanCalculator.Data.EntityFramework.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LoanTypes",
                columns: table => new
                {
                    Key = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    InterestRate = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanTypes", x => x.Key);
                });

            migrationBuilder.InsertData(
                table: "LoanTypes",
                columns: new[] { "Key", "Description", "InterestRate" },
                values: new object[] { "house", "House loan", 0.035f });

            migrationBuilder.InsertData(
                table: "LoanTypes",
                columns: new[] { "Key", "Description", "InterestRate" },
                values: new object[] { "car", "Car loan", 0.065f });

            migrationBuilder.InsertData(
                table: "LoanTypes",
                columns: new[] { "Key", "Description", "InterestRate" },
                values: new object[] { "consumer", "Consumer loan", 0.105f });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoanTypes");
        }
    }
}
