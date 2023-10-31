using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebVisit.Migrations.CzSVISIT
{
    /// <inheritdoc />
    public partial class commoncode9 : Migration
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

            migrationBuilder.InsertData(
                table: "CommonCode",
                columns: new[] { "CommonCodeID", "CodeNameEng", "CodeNameKor", "GroupNo", "InsertName", "InsertOrgID", "InsertOrgNameEng", "InsertOrgNameKor", "InsertSabun", "IsActive", "IsSys", "Memo", "OrderNo", "SubCodeDesc", "SubCodeID", "UpdateDate" },
                values: new object[,]
                {
                    { 277, "Export Import Apply Status", "반출입신청상태", 277, null, null, null, null, "1", "Y", "Y", null, 0, null, null, null },
                    { 278, "Application", "신청", 277, null, null, null, null, "1", "Y", "Y", null, 1, null, 0, null },
                    { 279, "Approved", "승인", 277, null, null, null, null, "1", "Y", "Y", null, 2, null, 1, null },
                    { 280, "Companion", "반려", 277, null, null, null, null, "1", "Y", "Y", null, 3, null, 2, null },
                    { 281, "Under Approval", "결재중", 277, null, null, null, null, "1", "Y", "Y", null, 4, null, 3, null },
                    { 282, "Payment Completed", "결재완료", 277, null, null, null, null, "1", "Y", "Y", null, 5, null, 4, null },
                    { 283, "Unapproved", "미승인", 277, null, null, null, null, "1", "Y", "Y", null, 6, null, 5, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CommonCode",
                keyColumn: "CommonCodeID",
                keyValue: 277);

            migrationBuilder.DeleteData(
                table: "CommonCode",
                keyColumn: "CommonCodeID",
                keyValue: 278);

            migrationBuilder.DeleteData(
                table: "CommonCode",
                keyColumn: "CommonCodeID",
                keyValue: 279);

            migrationBuilder.DeleteData(
                table: "CommonCode",
                keyColumn: "CommonCodeID",
                keyValue: 280);

            migrationBuilder.DeleteData(
                table: "CommonCode",
                keyColumn: "CommonCodeID",
                keyValue: 281);

            migrationBuilder.DeleteData(
                table: "CommonCode",
                keyColumn: "CommonCodeID",
                keyValue: 282);

            migrationBuilder.DeleteData(
                table: "CommonCode",
                keyColumn: "CommonCodeID",
                keyValue: 283);

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
