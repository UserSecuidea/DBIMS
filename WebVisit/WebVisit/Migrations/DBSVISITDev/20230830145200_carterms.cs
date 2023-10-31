using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebVisit.Migrations.DBSVISITDev
{
    /// <inheritdoc />
    public partial class carterms : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IsTermsVehicle",
                table: "VisitApplyPersonHistory",
                type: "char(1)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IsTermsVehicle",
                table: "VisitApplyPerson",
                type: "char(1)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsTermsVehicle",
                table: "VisitApplyPersonHistory");

            migrationBuilder.DropColumn(
                name: "IsTermsVehicle",
                table: "VisitApplyPerson");
        }
    }
}
