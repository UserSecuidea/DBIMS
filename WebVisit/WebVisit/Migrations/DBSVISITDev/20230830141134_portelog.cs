using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebVisit.Migrations.DBSVISITDev
{
    /// <inheritdoc />
    public partial class portelog : Migration
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
                keyValue: 148,
                columns: new[] { "CodeNameEng", "CodeNameKor", "IsActive", "IsDelete" },
                values: new object[] { "Unapproved", "미승인", "Y", "N" });

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PorteLog");

            migrationBuilder.UpdateData(
                table: "CommonCode",
                keyColumn: "CommonCodeID",
                keyValue: 148,
                columns: new[] { "CodeNameEng", "CodeNameKor", "IsActive", "IsDelete" },
                values: new object[] { "Not Returned", "미반납", "N", "Y" });

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
                    { 268, "Lost AccessCard", "출입증분실", 150, null, null, null, null, "1", "Y", "Y", null, 6, null, 5, null }
                });
        }
    }
}
