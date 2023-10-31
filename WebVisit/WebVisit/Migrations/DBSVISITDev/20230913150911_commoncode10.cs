using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebVisit.Migrations.DBSVISITDev
{
    /// <inheritdoc />
    public partial class commoncode10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "ImportCnt",
                table: "ExportImportItem",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "RemainCnt",
                table: "ExportImportItem",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ExportImportApplyStatus",
                table: "ExportImportHistory",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ExportImportApplyStatus",
                table: "ExportImport",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ExportImportItemHistory",
                columns: table => new
                {
                    ExportImportItemHistoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExportImportItemID = table.Column<int>(type: "int", nullable: false),
                    ExportImportID = table.Column<int>(type: "int", nullable: false),
                    PRNo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    MaterialsCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    MaterialsName = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Quantity = table.Column<float>(type: "real", nullable: true),
                    Unit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ImportCnt = table.Column<float>(type: "real", nullable: true),
                    RemainCnt = table.Column<float>(type: "real", nullable: true),
                    Standard = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Memo = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<string>(type: "char(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExportImportItemHistory", x => x.ExportImportItemHistoryID);
                });

            migrationBuilder.UpdateData(
                table: "CommonCode",
                keyColumn: "CommonCodeID",
                keyValue: 163,
                column: "IsDelete",
                value: "Y");

            migrationBuilder.UpdateData(
                table: "CommonCode",
                keyColumn: "CommonCodeID",
                keyValue: 164,
                column: "IsDelete",
                value: "Y");

            migrationBuilder.UpdateData(
                table: "CommonCode",
                keyColumn: "CommonCodeID",
                keyValue: 165,
                column: "IsDelete",
                value: "Y");

            migrationBuilder.UpdateData(
                table: "CommonCode",
                keyColumn: "CommonCodeID",
                keyValue: 166,
                column: "IsDelete",
                value: "Y");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExportImportItemHistory");

            migrationBuilder.DropColumn(
                name: "ImportCnt",
                table: "ExportImportItem");

            migrationBuilder.DropColumn(
                name: "RemainCnt",
                table: "ExportImportItem");

            migrationBuilder.DropColumn(
                name: "ExportImportApplyStatus",
                table: "ExportImportHistory");

            migrationBuilder.DropColumn(
                name: "ExportImportApplyStatus",
                table: "ExportImport");

            migrationBuilder.UpdateData(
                table: "CommonCode",
                keyColumn: "CommonCodeID",
                keyValue: 163,
                column: "IsDelete",
                value: "N");

            migrationBuilder.UpdateData(
                table: "CommonCode",
                keyColumn: "CommonCodeID",
                keyValue: 164,
                column: "IsDelete",
                value: "N");

            migrationBuilder.UpdateData(
                table: "CommonCode",
                keyColumn: "CommonCodeID",
                keyValue: 165,
                column: "IsDelete",
                value: "N");

            migrationBuilder.UpdateData(
                table: "CommonCode",
                keyColumn: "CommonCodeID",
                keyValue: 166,
                column: "IsDelete",
                value: "N");
        }
    }
}
