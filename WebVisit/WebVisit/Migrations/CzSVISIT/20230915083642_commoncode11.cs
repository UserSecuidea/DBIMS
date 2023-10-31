using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebVisit.Migrations.CzSVISIT
{
    /// <inheritdoc />
    public partial class commoncode11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "CommonCode",
                keyColumn: "CommonCodeID",
                keyValue: 279,
                columns: new[] { "CodeNameEng", "CodeNameKor" },
                values: new object[] { "Payment Completed", "결재완료 " });

            migrationBuilder.UpdateData(
                table: "CommonCode",
                keyColumn: "CommonCodeID",
                keyValue: 282,
                columns: new[] { "CodeNameEng", "CodeNameKor" },
                values: new object[] { "Approved", "승인완료" });

            migrationBuilder.InsertData(
                table: "CommonCode",
                columns: new[] { "CommonCodeID", "CodeNameEng", "CodeNameKor", "GroupNo", "InsertName", "InsertOrgID", "InsertOrgNameEng", "InsertOrgNameKor", "InsertSabun", "IsActive", "IsSys", "Memo", "OrderNo", "SubCodeDesc", "SubCodeID", "UpdateDate" },
                values: new object[,]
                {
                    { 284, "Approval Request", "상신", 277, null, null, null, null, "1", "Y", "Y", null, 7, null, 64, null },
                    { 285, "Temporary Save", "임시저장", 277, null, null, null, null, "1", "Y", "Y", null, 8, null, 256, null },
                    { 286, "Approval Pending", "승인보류", 277, null, null, null, null, "1", "Y", "Y", null, 9, null, 512, null },
                    { 287, "Delete", "삭제", 277, null, null, null, null, "1", "Y", "Y", null, 10, null, 1024, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.UpdateData(
                table: "CommonCode",
                keyColumn: "CommonCodeID",
                keyValue: 279,
                columns: new[] { "CodeNameEng", "CodeNameKor" },
                values: new object[] { "Approved", "승인" });

            migrationBuilder.UpdateData(
                table: "CommonCode",
                keyColumn: "CommonCodeID",
                keyValue: 282,
                columns: new[] { "CodeNameEng", "CodeNameKor" },
                values: new object[] { "Payment Completed", "결재완료" });
        }
    }
}
