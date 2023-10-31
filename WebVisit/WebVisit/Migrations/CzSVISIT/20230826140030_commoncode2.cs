using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebVisit.Migrations.CzSVISIT
{
    /// <inheritdoc />
    public partial class commoncode2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "CommonCode",
                keyColumn: "CommonCodeID",
                keyValue: 262,
                columns: new[] { "CodeNameEng", "CodeNameKor", "GroupNo", "IsDelete", "IsSys", "OrderNo", "SubCodeID" },
                values: new object[] { "Important Buyer", "주요고객사", 116, "Y", "N", 2, 1 });

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
                    { 268, "Lost AccessCard", "출입증분실", 150, null, null, null, null, "1", "Y", "Y", null, 6, null, 5, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.UpdateData(
                table: "CommonCode",
                keyColumn: "CommonCodeID",
                keyValue: 262,
                columns: new[] { "CodeNameEng", "CodeNameKor", "GroupNo", "IsDelete", "IsSys", "OrderNo", "SubCodeID" },
                values: new object[] { "Lost AccessCard", "출입증분실", 150, "N", "Y", 6, 5 });
        }
    }
}
