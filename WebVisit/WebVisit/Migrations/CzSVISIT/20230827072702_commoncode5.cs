using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebVisit.Migrations.CzSVISIT
{
    /// <inheritdoc />
    public partial class commoncode5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "CommonCode",
                keyColumn: "CommonCodeID",
                keyValue: 170,
                columns: new[] { "CodeNameEng", "CodeNameKor", "SubCodeDesc" },
                values: new object[] { "Carry In Completed", "반입완료", "" });

            migrationBuilder.UpdateData(
                table: "CommonCode",
                keyColumn: "CommonCodeID",
                keyValue: 172,
                columns: new[] { "CodeNameEng", "CodeNameKor", "SubCodeDesc" },
                values: new object[] { "TakeOut", "반출요", "" });

            migrationBuilder.UpdateData(
                table: "CommonCode",
                keyColumn: "CommonCodeID",
                keyValue: 173,
                columns: new[] { "CodeNameEng", "CodeNameKor" },
                values: new object[] { "Do Not TakeOut", "반출불필요(유상)" });

            migrationBuilder.UpdateData(
                table: "CommonCode",
                keyColumn: "CommonCodeID",
                keyValue: 174,
                columns: new[] { "CodeNameEng", "CodeNameKor" },
                values: new object[] { "Do Not TakeOut(No)", "반출불필요(무상)" });

            migrationBuilder.UpdateData(
                table: "CommonCode",
                keyColumn: "CommonCodeID",
                keyValue: 175,
                columns: new[] { "CodeNameEng", "CodeNameKor" },
                values: new object[] { "PreOrder", "선입고" });

            migrationBuilder.UpdateData(
                table: "CommonCode",
                keyColumn: "CommonCodeID",
                keyValue: 176,
                columns: new[] { "CodeNameEng", "CodeNameKor", "SubCodeDesc" },
                values: new object[] { "Waiting Take Out", "반출대기", "13002" });

            migrationBuilder.InsertData(
                table: "CommonCode",
                columns: new[] { "CommonCodeID", "CodeNameEng", "CodeNameKor", "GroupNo", "InsertName", "InsertOrgID", "InsertOrgNameEng", "InsertOrgNameKor", "InsertSabun", "IsActive", "IsSys", "Memo", "OrderNo", "SubCodeDesc", "SubCodeID", "UpdateDate" },
                values: new object[] { 269, "Transfer Completed", "반출완료", 168, null, null, null, null, "1", "Y", "Y", null, 9, "13004", 8, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CommonCode",
                keyColumn: "CommonCodeID",
                keyValue: 269);

            migrationBuilder.UpdateData(
                table: "CommonCode",
                keyColumn: "CommonCodeID",
                keyValue: 170,
                columns: new[] { "CodeNameEng", "CodeNameKor", "SubCodeDesc" },
                values: new object[] { "Waiting Take Out", "반출대기", "13002" });

            migrationBuilder.UpdateData(
                table: "CommonCode",
                keyColumn: "CommonCodeID",
                keyValue: 172,
                columns: new[] { "CodeNameEng", "CodeNameKor", "SubCodeDesc" },
                values: new object[] { "Transfer Completed", "반출완료", "13004" });

            migrationBuilder.UpdateData(
                table: "CommonCode",
                keyColumn: "CommonCodeID",
                keyValue: 173,
                columns: new[] { "CodeNameEng", "CodeNameKor" },
                values: new object[] { "Not Bring", "미반입" });

            migrationBuilder.UpdateData(
                table: "CommonCode",
                keyColumn: "CommonCodeID",
                keyValue: 174,
                columns: new[] { "CodeNameEng", "CodeNameKor" },
                values: new object[] { "Not Submitted", "미반출" });

            migrationBuilder.UpdateData(
                table: "CommonCode",
                keyColumn: "CommonCodeID",
                keyValue: 175,
                columns: new[] { "CodeNameEng", "CodeNameKor" },
                values: new object[] { "Carry In Completed", "반입완료" });

            migrationBuilder.UpdateData(
                table: "CommonCode",
                keyColumn: "CommonCodeID",
                keyValue: 176,
                columns: new[] { "CodeNameEng", "CodeNameKor", "SubCodeDesc" },
                values: new object[] { "Confirmation Completed", "확인완료", "" });
        }
    }
}
