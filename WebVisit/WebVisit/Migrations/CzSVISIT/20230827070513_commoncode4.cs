using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebVisit.Migrations.CzSVISIT
{
    /// <inheritdoc />
    public partial class commoncode4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "CommonCode",
                keyColumn: "CommonCodeID",
                keyValue: 148,
                column: "IsActive",
                value: "Y");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "CommonCode",
                keyColumn: "CommonCodeID",
                keyValue: 148,
                column: "IsActive",
                value: "N");
        }
    }
}
