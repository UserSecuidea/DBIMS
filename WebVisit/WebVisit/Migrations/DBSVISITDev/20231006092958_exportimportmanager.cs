using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebVisit.Migrations.DBSVISITDev
{
    /// <inheritdoc />
    public partial class exportimportmanager : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ManagerName",
                table: "ExportImportHistory",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ManagerOrgID",
                table: "ExportImportHistory",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ManagerOrgNameEng",
                table: "ExportImportHistory",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ManagerOrgNameKor",
                table: "ExportImportHistory",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ManagerSabun",
                table: "ExportImportHistory",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ManagerTel",
                table: "ExportImportHistory",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ManagerName",
                table: "ExportImport",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ManagerOrgID",
                table: "ExportImport",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ManagerOrgNameEng",
                table: "ExportImport",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ManagerOrgNameKor",
                table: "ExportImport",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ManagerSabun",
                table: "ExportImport",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ManagerTel",
                table: "ExportImport",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ManagerName",
                table: "ExportImportHistory");

            migrationBuilder.DropColumn(
                name: "ManagerOrgID",
                table: "ExportImportHistory");

            migrationBuilder.DropColumn(
                name: "ManagerOrgNameEng",
                table: "ExportImportHistory");

            migrationBuilder.DropColumn(
                name: "ManagerOrgNameKor",
                table: "ExportImportHistory");

            migrationBuilder.DropColumn(
                name: "ManagerSabun",
                table: "ExportImportHistory");

            migrationBuilder.DropColumn(
                name: "ManagerTel",
                table: "ExportImportHistory");

            migrationBuilder.DropColumn(
                name: "ManagerName",
                table: "ExportImport");

            migrationBuilder.DropColumn(
                name: "ManagerOrgID",
                table: "ExportImport");

            migrationBuilder.DropColumn(
                name: "ManagerOrgNameEng",
                table: "ExportImport");

            migrationBuilder.DropColumn(
                name: "ManagerOrgNameKor",
                table: "ExportImport");

            migrationBuilder.DropColumn(
                name: "ManagerSabun",
                table: "ExportImport");

            migrationBuilder.DropColumn(
                name: "ManagerTel",
                table: "ExportImport");
        }
    }
}
