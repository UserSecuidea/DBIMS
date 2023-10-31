using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebVisit.Migrations.DBSVISITDev
{
    /// <inheritdoc />
    public partial class carryitemapply : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IsVaccineUpdated",
                table: "CarryItemApply",
                type: "char(1)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IsVirusScanned",
                table: "CarryItemApply",
                type: "char(1)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "QuarantineConfirmationContact",
                table: "CarryItemApply",
                type: "char(1)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "QuarantineConfirmationGate",
                table: "CarryItemApply",
                type: "char(1)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsVaccineUpdated",
                table: "CarryItemApply");

            migrationBuilder.DropColumn(
                name: "IsVirusScanned",
                table: "CarryItemApply");

            migrationBuilder.DropColumn(
                name: "QuarantineConfirmationContact",
                table: "CarryItemApply");

            migrationBuilder.DropColumn(
                name: "QuarantineConfirmationGate",
                table: "CarryItemApply");
        }
    }
}
