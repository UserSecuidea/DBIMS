using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebVisit.Migrations.CzSVISIT
{
    /// <inheritdoc />
    public partial class commoncode12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "CommonCode",
                columns: new[] { "CommonCodeID", "CodeNameEng", "CodeNameKor", "GroupNo", "InsertName", "InsertOrgID", "InsertOrgNameEng", "InsertOrgNameKor", "InsertSabun", "IsActive", "IsSys", "Memo", "OrderNo", "SubCodeDesc", "SubCodeID", "UpdateDate" },
                values: new object[] { 292, "Verification Cancel", "확인취소", 162, null, null, null, null, "1", "Y", "Y", null, 13, null, 12, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CommonCode",
                keyColumn: "CommonCodeID",
                keyValue: 292);
        }
    }
}
