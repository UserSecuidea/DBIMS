using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebVisit.Migrations.DBSVISITDev
{
    /// <inheritdoc />
    public partial class carryiteminfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "IsVaccineUpdated",
                table: "CarryItemInfo",
                type: "char(1)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IsVirusScanned",
                table: "CarryItemInfo",
                type: "char(1)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "QuarantineConfirmationContact",
                table: "CarryItemInfo",
                type: "char(1)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "QuarantineConfirmationGate",
                table: "CarryItemInfo",
                type: "char(1)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsVaccineUpdated",
                table: "CarryItemInfo");

            migrationBuilder.DropColumn(
                name: "IsVirusScanned",
                table: "CarryItemInfo");

            migrationBuilder.DropColumn(
                name: "QuarantineConfirmationContact",
                table: "CarryItemInfo");

            migrationBuilder.DropColumn(
                name: "QuarantineConfirmationGate",
                table: "CarryItemInfo");

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
    }
}
