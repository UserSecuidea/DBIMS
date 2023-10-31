using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebVisit.Migrations.CzSVISIT
{
    /// <inheritdoc />
    public partial class commoncode3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "CommonCode",
                keyColumn: "CommonCodeID",
                keyValue: 148,
                columns: new[] { "CodeNameEng", "CodeNameKor", "IsDelete" },
                values: new object[] { "Unapproved", "미승인", "N" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "CommonCode",
                keyColumn: "CommonCodeID",
                keyValue: 148,
                columns: new[] { "CodeNameEng", "CodeNameKor", "IsDelete" },
                values: new object[] { "Not Returned", "미반납", "Y" });
        }
    }
}
