using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebVisit.Migrations.DBSVISITDev
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
                table: "Level",
                columns: new[] { "LevelID", "InsertName", "InsertOrgID", "InsertOrgNameEng", "InsertOrgNameKor", "InsertSabun", "LevelName", "UpdateDate" },
                values: new object[] { 10, null, null, null, null, "1", "임직원(구매)", null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
