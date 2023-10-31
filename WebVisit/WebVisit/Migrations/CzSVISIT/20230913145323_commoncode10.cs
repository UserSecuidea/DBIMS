using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebVisit.Migrations.CzSVISIT
{
    /// <inheritdoc />
    public partial class commoncode10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExportImportItemHistory");
        }
    }
}
