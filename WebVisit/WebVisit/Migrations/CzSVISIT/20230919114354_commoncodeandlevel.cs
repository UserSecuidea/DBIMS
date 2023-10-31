using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebVisit.Migrations.CzSVISIT
{
    /// <inheritdoc />
    public partial class commoncodeandlevel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "CommonCode",
                keyColumn: "CommonCodeID",
                keyValue: 179,
                columns: new[] { "CodeNameEng", "CodeNameKor" },
                values: new object[] { "Payment Completed", "결재완료 " });

            migrationBuilder.UpdateData(
                table: "CommonCode",
                keyColumn: "CommonCodeID",
                keyValue: 182,
                columns: new[] { "CodeNameEng", "CodeNameKor" },
                values: new object[] { "Approved", "승인완료" });

            migrationBuilder.InsertData(
                table: "CommonCode",
                columns: new[] { "CommonCodeID", "CodeNameEng", "CodeNameKor", "GroupNo", "InsertName", "InsertOrgID", "InsertOrgNameEng", "InsertOrgNameKor", "InsertSabun", "IsActive", "IsSys", "Memo", "OrderNo", "SubCodeDesc", "SubCodeID", "UpdateDate" },
                values: new object[,]
                {
                    { 288, "Approval Request", "상신", 177, null, null, null, null, "1", "Y", "Y", null, 7, null, 64, null },
                    { 289, "Temporary Save", "임시저장", 177, null, null, null, null, "1", "Y", "Y", null, 8, null, 256, null },
                    { 290, "Approval Pending", "승인보류", 177, null, null, null, null, "1", "Y", "Y", null, 9, null, 512, null },
                    { 291, "Delete", "삭제", 177, null, null, null, null, "1", "Y", "Y", null, 10, null, 1024, null }
                });

            migrationBuilder.InsertData(
                table: "Level",
                columns: new[] { "LevelID", "InsertName", "InsertOrgID", "InsertOrgNameEng", "InsertOrgNameKor", "InsertSabun", "LevelName", "UpdateDate" },
                values: new object[] { 10, null, null, null, null, "1", "임직원(구매)", null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                table: "Level",
                keyColumn: "LevelID",
                keyValue: 10);

            migrationBuilder.UpdateData(
                table: "CommonCode",
                keyColumn: "CommonCodeID",
                keyValue: 179,
                columns: new[] { "CodeNameEng", "CodeNameKor" },
                values: new object[] { "Approved", "승인" });

            migrationBuilder.UpdateData(
                table: "CommonCode",
                keyColumn: "CommonCodeID",
                keyValue: 182,
                columns: new[] { "CodeNameEng", "CodeNameKor" },
                values: new object[] { "Payment Completed", "결재완료" });
        }
    }
}
