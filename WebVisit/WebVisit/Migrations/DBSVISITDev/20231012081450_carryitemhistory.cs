using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebVisit.Migrations.DBSVISITDev
{
    /// <inheritdoc />
    public partial class carryitemhistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IsVaccineUpdated",
                table: "CarryItemInfoHistory",
                type: "char(1)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IsVirusScanned",
                table: "CarryItemInfoHistory",
                type: "char(1)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "QuarantineConfirmationContact",
                table: "CarryItemInfoHistory",
                type: "char(1)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "QuarantineConfirmationGate",
                table: "CarryItemInfoHistory",
                type: "char(1)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsVaccineUpdated",
                table: "CarryItemInfoHistory");

            migrationBuilder.DropColumn(
                name: "IsVirusScanned",
                table: "CarryItemInfoHistory");

            migrationBuilder.DropColumn(
                name: "QuarantineConfirmationContact",
                table: "CarryItemInfoHistory");

            migrationBuilder.DropColumn(
                name: "QuarantineConfirmationGate",
                table: "CarryItemInfoHistory");
        }
    }
}
