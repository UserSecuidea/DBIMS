using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebVisit.Migrations.CzSVISIT
{
    /// <inheritdoc />
    public partial class commoncode6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "CommonCode",
                keyColumn: "CommonCodeID",
                keyValue: 163,
                columns: new[] { "CodeNameEng", "CodeNameKor" },
                values: new object[] { "Application", "신청" });

            migrationBuilder.UpdateData(
                table: "CommonCode",
                keyColumn: "CommonCodeID",
                keyValue: 164,
                columns: new[] { "CodeNameEng", "CodeNameKor" },
                values: new object[] { "Companion", "반려" });

            migrationBuilder.UpdateData(
                table: "CommonCode",
                keyColumn: "CommonCodeID",
                keyValue: 165,
                columns: new[] { "CodeNameEng", "CodeNameKor" },
                values: new object[] { "Waiting Export", "반출대기" });

            migrationBuilder.UpdateData(
                table: "CommonCode",
                keyColumn: "CommonCodeID",
                keyValue: 166,
                columns: new[] { "CodeNameEng", "CodeNameKor" },
                values: new object[] { "Export Confirmation", "반출확인" });

            migrationBuilder.UpdateData(
                table: "CommonCode",
                keyColumn: "CommonCodeID",
                keyValue: 167,
                columns: new[] { "CodeNameEng", "CodeNameKor" },
                values: new object[] { "Return Front Door", "정문반송" });

            migrationBuilder.UpdateData(
                table: "CommonCode",
                keyColumn: "CommonCodeID",
                keyValue: 259,
                columns: new[] { "CodeNameEng", "CodeNameKor" },
                values: new object[] { "Export Completed", "반출완료" });

            migrationBuilder.UpdateData(
                table: "CommonCode",
                keyColumn: "CommonCodeID",
                keyValue: 260,
                columns: new[] { "CodeNameEng", "CodeNameKor" },
                values: new object[] { "Waiting Import", "반입대기" });

            migrationBuilder.InsertData(
                table: "CommonCode",
                columns: new[] { "CommonCodeID", "CodeNameEng", "CodeNameKor", "GroupNo", "InsertName", "InsertOrgID", "InsertOrgNameEng", "InsertOrgNameKor", "InsertSabun", "IsActive", "IsSys", "Memo", "OrderNo", "SubCodeDesc", "SubCodeID", "UpdateDate" },
                values: new object[,]
                {
                    { 270, "Import Confirmation", "반입확인", 162, null, null, null, null, "1", "Y", "Y", null, 8, null, 7, null },
                    { 271, "Import Completed", "반입완료", 162, null, null, null, null, "1", "Y", "Y", null, 9, null, 8, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CommonCode",
                keyColumn: "CommonCodeID",
                keyValue: 270);

            migrationBuilder.DeleteData(
                table: "CommonCode",
                keyColumn: "CommonCodeID",
                keyValue: 271);

            migrationBuilder.UpdateData(
                table: "CommonCode",
                keyColumn: "CommonCodeID",
                keyValue: 163,
                columns: new[] { "CodeNameEng", "CodeNameKor" },
                values: new object[] { "Waiting Approval", "승인대기" });

            migrationBuilder.UpdateData(
                table: "CommonCode",
                keyColumn: "CommonCodeID",
                keyValue: 164,
                columns: new[] { "CodeNameEng", "CodeNameKor" },
                values: new object[] { "Approved", "승인완료" });

            migrationBuilder.UpdateData(
                table: "CommonCode",
                keyColumn: "CommonCodeID",
                keyValue: 165,
                columns: new[] { "CodeNameEng", "CodeNameKor" },
                values: new object[] { "Companion", "반려" });

            migrationBuilder.UpdateData(
                table: "CommonCode",
                keyColumn: "CommonCodeID",
                keyValue: 166,
                columns: new[] { "CodeNameEng", "CodeNameKor" },
                values: new object[] { "Under Approval", "결재중" });

            migrationBuilder.UpdateData(
                table: "CommonCode",
                keyColumn: "CommonCodeID",
                keyValue: 167,
                columns: new[] { "CodeNameEng", "CodeNameKor" },
                values: new object[] { "Payment Completed", "결재완료" });

            migrationBuilder.UpdateData(
                table: "CommonCode",
                keyColumn: "CommonCodeID",
                keyValue: 259,
                columns: new[] { "CodeNameEng", "CodeNameKor" },
                values: new object[] { "Import Confirmation", "반입확인" });

            migrationBuilder.UpdateData(
                table: "CommonCode",
                keyColumn: "CommonCodeID",
                keyValue: 260,
                columns: new[] { "CodeNameEng", "CodeNameKor" },
                values: new object[] { "Export Confirmation", "반출확인" });
        }
    }
}
