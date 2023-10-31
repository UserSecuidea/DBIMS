using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebVisit.Migrations.CzSVISIT
{
    /// <inheritdoc />
    public partial class commoncode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "CommonCode",
                columns: new[] { "CommonCodeID", "CodeNameEng", "CodeNameKor", "GroupNo", "InsertName", "InsertOrgID", "InsertOrgNameEng", "InsertOrgNameKor", "InsertSabun", "IsActive", "IsSys", "Memo", "OrderNo", "SubCodeDesc", "SubCodeID", "UpdateDate" },
                values: new object[] { 262, "Lost AccessCard", "출입증분실", 150, null, null, null, null, "1", "Y", "Y", null, 6, null, 5, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CommonCode",
                keyColumn: "CommonCodeID",
                keyValue: 262);
        }
    }
}
