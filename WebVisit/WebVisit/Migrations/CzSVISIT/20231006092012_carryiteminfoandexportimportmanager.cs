using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebVisit.Migrations.CzSVISIT
{
    /// <inheritdoc />
    public partial class carryiteminfoandexportimportmanager : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CommonCode",
                keyColumn: "CommonCodeID",
                keyValue: 262);

            migrationBuilder.DeleteData(
                table: "CommonCode",
                keyColumn: "CommonCodeID",
                keyValue: 263);

            migrationBuilder.DeleteData(
                table: "CommonCode",
                keyColumn: "CommonCodeID",
                keyValue: 264);

            migrationBuilder.DeleteData(
                table: "CommonCode",
                keyColumn: "CommonCodeID",
                keyValue: 265);

            migrationBuilder.DeleteData(
                table: "CommonCode",
                keyColumn: "CommonCodeID",
                keyValue: 266);

            migrationBuilder.DeleteData(
                table: "CommonCode",
                keyColumn: "CommonCodeID",
                keyValue: 267);

            migrationBuilder.DeleteData(
                table: "CommonCode",
                keyColumn: "CommonCodeID",
                keyValue: 268);

            migrationBuilder.DeleteData(
                table: "CommonCode",
                keyColumn: "CommonCodeID",
                keyValue: 269);

            migrationBuilder.DeleteData(
                table: "CommonCode",
                keyColumn: "CommonCodeID",
                keyValue: 270);

            migrationBuilder.DeleteData(
                table: "CommonCode",
                keyColumn: "CommonCodeID",
                keyValue: 271);

            migrationBuilder.DeleteData(
                table: "CommonCode",
                keyColumn: "CommonCodeID",
                keyValue: 272);

            migrationBuilder.DeleteData(
                table: "CommonCode",
                keyColumn: "CommonCodeID",
                keyValue: 273);

            migrationBuilder.DeleteData(
                table: "CommonCode",
                keyColumn: "CommonCodeID",
                keyValue: 274);

            migrationBuilder.DeleteData(
                table: "CommonCode",
                keyColumn: "CommonCodeID",
                keyValue: 275);

            migrationBuilder.DeleteData(
                table: "CommonCode",
                keyColumn: "CommonCodeID",
                keyValue: 276);

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

            migrationBuilder.DeleteData(
                table: "CommonCode",
                keyColumn: "CommonCodeID",
                keyValue: 284);

            migrationBuilder.DeleteData(
                table: "CommonCode",
                keyColumn: "CommonCodeID",
                keyValue: 285);

            migrationBuilder.DeleteData(
                table: "CommonCode",
                keyColumn: "CommonCodeID",
                keyValue: 286);

            migrationBuilder.DeleteData(
                table: "CommonCode",
                keyColumn: "CommonCodeID",
                keyValue: 287);

            migrationBuilder.DeleteData(
                table: "CommonCode",
                keyColumn: "CommonCodeID",
                keyValue: 288);

            migrationBuilder.DeleteData(
                table: "CommonCode",
                keyColumn: "CommonCodeID",
                keyValue: 289);

            migrationBuilder.DeleteData(
                table: "CommonCode",
                keyColumn: "CommonCodeID",
                keyValue: 290);

            migrationBuilder.DeleteData(
                table: "CommonCode",
                keyColumn: "CommonCodeID",
                keyValue: 291);

            migrationBuilder.DeleteData(
                table: "CommonCode",
                keyColumn: "CommonCodeID",
                keyValue: 292);

            migrationBuilder.DropColumn(
                name: "IsVaccineUpdated",
                table: "CarryItemApply");

            migrationBuilder.DropColumn(
                name: "IsVirusScanned",
                table: "CarryItemApply");

            migrationBuilder.DropColumn(
                name: "QuarantineConfirmationContact",
                table: "CarryItemApply");

            migrationBuilder.DropColumn(
                name: "QuarantineConfirmationGate",
                table: "CarryItemApply");

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

            migrationBuilder.AddColumn<string>(
                name: "IsVaccineUpdated",
                table: "CarryItemInfo",
                type: "char(1)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IsVirusScanned",
                table: "CarryItemInfo",
                type: "char(1)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "QuarantineConfirmationContact",
                table: "CarryItemInfo",
                type: "char(1)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "QuarantineConfirmationGate",
                table: "CarryItemInfo",
                type: "char(1)",
                nullable: false,
                defaultValue: "");
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

            migrationBuilder.DropColumn(
                name: "IsVaccineUpdated",
                table: "CarryItemInfo");

            migrationBuilder.DropColumn(
                name: "IsVirusScanned",
                table: "CarryItemInfo");

            migrationBuilder.DropColumn(
                name: "QuarantineConfirmationContact",
                table: "CarryItemInfo");

            migrationBuilder.DropColumn(
                name: "QuarantineConfirmationGate",
                table: "CarryItemInfo");

            migrationBuilder.AddColumn<string>(
                name: "IsVaccineUpdated",
                table: "CarryItemApply",
                type: "char(1)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IsVirusScanned",
                table: "CarryItemApply",
                type: "char(1)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "QuarantineConfirmationContact",
                table: "CarryItemApply",
                type: "char(1)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "QuarantineConfirmationGate",
                table: "CarryItemApply",
                type: "char(1)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "CommonCode",
                columns: new[] { "CommonCodeID", "CodeNameEng", "CodeNameKor", "GroupNo", "InsertName", "InsertOrgID", "InsertOrgNameEng", "InsertOrgNameKor", "InsertSabun", "IsActive", "IsDelete", "IsSys", "Memo", "OrderNo", "SubCodeDesc", "SubCodeID", "UpdateDate" },
                values: new object[] { 262, "Important Buyer", "주요고객사", 116, null, null, null, null, "1", "Y", "Y", "N", null, 2, null, 1, null });

            migrationBuilder.InsertData(
                table: "CommonCode",
                columns: new[] { "CommonCodeID", "CodeNameEng", "CodeNameKor", "GroupNo", "InsertName", "InsertOrgID", "InsertOrgNameEng", "InsertOrgNameKor", "InsertSabun", "IsActive", "IsSys", "Memo", "OrderNo", "SubCodeDesc", "SubCodeID", "UpdateDate" },
                values: new object[] { 263, "Executive", "임원", 116, null, null, null, null, "1", "Y", "N", null, 3, null, 2, null });

            migrationBuilder.InsertData(
                table: "CommonCode",
                columns: new[] { "CommonCodeID", "CodeNameEng", "CodeNameKor", "GroupNo", "InsertName", "InsertOrgID", "InsertOrgNameEng", "InsertOrgNameKor", "InsertSabun", "IsActive", "IsDelete", "IsSys", "Memo", "OrderNo", "SubCodeDesc", "SubCodeID", "UpdateDate" },
                values: new object[] { 264, "Government office", "관공서", 116, null, null, null, null, "1", "Y", "Y", "N", null, 4, null, 3, null });

            migrationBuilder.InsertData(
                table: "CommonCode",
                columns: new[] { "CommonCodeID", "CodeNameEng", "CodeNameKor", "GroupNo", "InsertName", "InsertOrgID", "InsertOrgNameEng", "InsertOrgNameKor", "InsertSabun", "IsActive", "IsSys", "Memo", "OrderNo", "SubCodeDesc", "SubCodeID", "UpdateDate" },
                values: new object[,]
                {
                    { 265, "Important Buyer", "주요고객사", 116, null, null, null, null, "1", "Y", "N", null, 5, null, 4, null },
                    { 266, "Government office", "관공서", 116, null, null, null, null, "1", "Y", "N", null, 6, null, 5, null },
                    { 267, "etc", "기타", 116, null, null, null, null, "1", "Y", "N", null, 7, null, 6, null },
                    { 268, "Lost AccessCard", "출입증분실", 150, null, null, null, null, "1", "Y", "Y", null, 6, null, 5, null },
                    { 269, "Transfer Completed", "반출완료", 168, null, null, null, null, "1", "Y", "Y", null, 9, "13004", 8, null },
                    { 270, "Export Completed", "반출완료", 162, null, null, null, null, "1", "Y", "Y", null, 8, null, 7, null },
                    { 271, "Import Approval Request", "반입상신", 162, null, null, null, null, "1", "Y", "Y", null, 9, null, 8, null },
                    { 272, "Waiting Import", "반입대기", 162, null, null, null, null, "1", "Y", "Y", null, 10, null, 9, null },
                    { 273, "Import Confirmation", "반입확인", 162, null, null, null, null, "1", "Y", "Y", null, 11, null, 10, null },
                    { 274, "Import Completed", "반입완료", 162, null, null, null, null, "1", "Y", "Y", null, 12, null, 11, null },
                    { 275, "Unapproved", "미승인", 177, null, null, null, null, "1", "Y", "Y", null, 6, null, 5, null },
                    { 276, "Not Import", "미반입", 168, null, null, null, null, "1", "Y", "Y", null, 10, null, 9, null },
                    { 277, "Export Import Apply Status", "반출입신청상태", 277, null, null, null, null, "1", "Y", "Y", null, 0, null, null, null },
                    { 278, "Application", "신청", 277, null, null, null, null, "1", "Y", "Y", null, 1, null, 0, null },
                    { 279, "Payment Completed", "결재완료 ", 277, null, null, null, null, "1", "Y", "Y", null, 2, null, 1, null },
                    { 280, "Companion", "반려", 277, null, null, null, null, "1", "Y", "Y", null, 3, null, 2, null },
                    { 281, "Under Approval", "결재중", 277, null, null, null, null, "1", "Y", "Y", null, 4, null, 3, null },
                    { 282, "Approved", "승인완료", 277, null, null, null, null, "1", "Y", "Y", null, 5, null, 4, null },
                    { 283, "Unapproved", "미승인", 277, null, null, null, null, "1", "Y", "Y", null, 6, null, 5, null },
                    { 284, "Approval Request", "상신", 277, null, null, null, null, "1", "Y", "Y", null, 7, null, 64, null },
                    { 285, "Temporary Save", "임시저장", 277, null, null, null, null, "1", "Y", "Y", null, 8, null, 256, null },
                    { 286, "Approval Pending", "승인보류", 277, null, null, null, null, "1", "Y", "Y", null, 9, null, 512, null },
                    { 287, "Delete", "삭제", 277, null, null, null, null, "1", "Y", "Y", null, 10, null, 1024, null },
                    { 288, "Approval Request", "상신", 177, null, null, null, null, "1", "Y", "Y", null, 7, null, 64, null },
                    { 289, "Temporary Save", "임시저장", 177, null, null, null, null, "1", "Y", "Y", null, 8, null, 256, null },
                    { 290, "Approval Pending", "승인보류", 177, null, null, null, null, "1", "Y", "Y", null, 9, null, 512, null },
                    { 291, "Delete", "삭제", 177, null, null, null, null, "1", "Y", "Y", null, 10, null, 1024, null },
                    { 292, "Verification Cancel", "확인취소", 162, null, null, null, null, "1", "Y", "Y", null, 13, null, 12, null }
                });
        }
    }
}
