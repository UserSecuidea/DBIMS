using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebVisit.Migrations.CzSVISIT
{
    /// <inheritdoc />
    public partial class exportimport3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "ExportImportItemHistory");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "ExportImportItemHistory");

            migrationBuilder.RenameColumn(
                name: "InsertDate",
                table: "ExportImportItemHistory",
                newName: "InsertUpdateDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InsertUpdateDate",
                table: "ExportImportItemHistory",
                newName: "InsertDate");

            migrationBuilder.AddColumn<string>(
                name: "IsDelete",
                table: "ExportImportItemHistory",
                type: "char(1)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "ExportImportItemHistory",
                type: "datetime2",
                nullable: true);
        }
    }
}
