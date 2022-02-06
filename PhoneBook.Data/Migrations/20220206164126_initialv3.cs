using Microsoft.EntityFrameworkCore.Migrations;

namespace PhoneBook.Data.Migrations
{
    public partial class initialv3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "ReportContents");

            migrationBuilder.DropColumn(
                name: "NumberInLocation",
                table: "ReportContents");

            migrationBuilder.DropColumn(
                name: "PersonInLocation",
                table: "ReportContents");

            migrationBuilder.AddColumn<string>(
                name: "ExcelLocation",
                table: "ReportContents",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExcelLocation",
                table: "ReportContents");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "ReportContents",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberInLocation",
                table: "ReportContents",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PersonInLocation",
                table: "ReportContents",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
