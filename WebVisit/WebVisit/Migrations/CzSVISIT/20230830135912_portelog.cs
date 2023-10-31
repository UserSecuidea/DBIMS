using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebVisit.Migrations.CzSVISIT
{
    /// <inheritdoc />
    public partial class portelog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PorteLog",
                columns: table => new
                {
                    PorteLogID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusinessID = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ResponseData = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    IsSuccess = table.Column<string>(type: "char(1)", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<string>(type: "char(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PorteLog", x => x.PorteLogID);
                });

            migrationBuilder.UpdateData(
                table: "CommonCode",
                keyColumn: "CommonCodeID",
                keyValue: 164,
                columns: new[] { "CodeNameEng", "CodeNameKor" },
                values: new object[] { "Export Approval Request", "반출상신" });

            migrationBuilder.UpdateData(
                table: "CommonCode",
                keyColumn: "CommonCodeID",
                keyValue: 165,
                columns: new[] { "CodeNameEng", "CodeNameKor" },
                values: new object[] { "Approval Complete", "결재완료" });

            migrationBuilder.UpdateData(
                table: "CommonCode",
                keyColumn: "CommonCodeID",
                keyValue: 166,
                columns: new[] { "CodeNameEng", "CodeNameKor" },
                values: new object[] { "Companion", "반려" });

            migrationBuilder.UpdateData(
                table: "CommonCode",
                keyColumn: "CommonCodeID",
                keyValue: 167,
                columns: new[] { "CodeNameEng", "CodeNameKor" },
                values: new object[] { "Waiting Export", "반출대기" });

            migrationBuilder.UpdateData(
                table: "CommonCode",
                keyColumn: "CommonCodeID",
                keyValue: 259,
                columns: new[] { "CodeNameEng", "CodeNameKor" },
                values: new object[] { "Export Confirmation", "반출확인" });

            migrationBuilder.UpdateData(
                table: "CommonCode",
                keyColumn: "CommonCodeID",
                keyValue: 260,
                columns: new[] { "CodeNameEng", "CodeNameKor" },
                values: new object[] { "Return Front Door", "정문반송" });

            migrationBuilder.UpdateData(
                table: "CommonCode",
                keyColumn: "CommonCodeID",
                keyValue: 270,
                columns: new[] { "CodeNameEng", "CodeNameKor" },
                values: new object[] { "Export Completed", "반출완료" });

            migrationBuilder.UpdateData(
                table: "CommonCode",
                keyColumn: "CommonCodeID",
                keyValue: 271,
                columns: new[] { "CodeNameEng", "CodeNameKor" },
                values: new object[] { "Import Approval Request", "반입상신" });

            migrationBuilder.InsertData(
                table: "CommonCode",
                columns: new[] { "CommonCodeID", "CodeNameEng", "CodeNameKor", "GroupNo", "InsertName", "InsertOrgID", "InsertOrgNameEng", "InsertOrgNameKor", "InsertSabun", "IsActive", "IsSys", "Memo", "OrderNo", "SubCodeDesc", "SubCodeID", "UpdateDate" },
                values: new object[,]
                {
                    { 272, "Waiting Import", "반입대기", 162, null, null, null, null, "1", "Y", "Y", null, 10, null, 9, null },
                    { 273, "Import Confirmation", "반입확인", 162, null, null, null, null, "1", "Y", "Y", null, 11, null, 10, null },
                    { 274, "Import Completed", "반입완료", 162, null, null, null, null, "1", "Y", "Y", null, 12, null, 11, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PorteLog");

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

            migrationBuilder.UpdateData(
                table: "CommonCode",
                keyColumn: "CommonCodeID",
                keyValue: 270,
                columns: new[] { "CodeNameEng", "CodeNameKor" },
                values: new object[] { "Import Confirmation", "반입확인" });

            migrationBuilder.UpdateData(
                table: "CommonCode",
                keyColumn: "CommonCodeID",
                keyValue: 271,
                columns: new[] { "CodeNameEng", "CodeNameKor" },
                values: new object[] { "Import Completed", "반입완료" });
        }
    }
}
