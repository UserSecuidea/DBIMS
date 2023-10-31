using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebVisit.Migrations.DBSVISITDev
{
    /// <inheritdoc />
    public partial class approvalinfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApprovalName",
                table: "WorkApplyHistory",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApprovalOrgID",
                table: "WorkApplyHistory",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApprovalOrgNameEng",
                table: "WorkApplyHistory",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApprovalOrgNameKor",
                table: "WorkApplyHistory",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApprovalSabun",
                table: "WorkApplyHistory",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApprovalTel",
                table: "WorkApplyHistory",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApprovalName",
                table: "WorkApply",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApprovalOrgID",
                table: "WorkApply",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApprovalOrgNameEng",
                table: "WorkApply",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApprovalOrgNameKor",
                table: "WorkApply",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApprovalSabun",
                table: "WorkApply",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApprovalTel",
                table: "WorkApply",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApprovalName",
                table: "CompanyHistory",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApprovalOrgID",
                table: "CompanyHistory",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApprovalOrgNameEng",
                table: "CompanyHistory",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApprovalOrgNameKor",
                table: "CompanyHistory",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApprovalSabun",
                table: "CompanyHistory",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApprovalTel",
                table: "CompanyHistory",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApprovalName",
                table: "Company",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApprovalOrgID",
                table: "Company",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApprovalOrgNameEng",
                table: "Company",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApprovalOrgNameKor",
                table: "Company",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApprovalSabun",
                table: "Company",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApprovalTel",
                table: "Company",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovalName",
                table: "WorkApplyHistory");

            migrationBuilder.DropColumn(
                name: "ApprovalOrgID",
                table: "WorkApplyHistory");

            migrationBuilder.DropColumn(
                name: "ApprovalOrgNameEng",
                table: "WorkApplyHistory");

            migrationBuilder.DropColumn(
                name: "ApprovalOrgNameKor",
                table: "WorkApplyHistory");

            migrationBuilder.DropColumn(
                name: "ApprovalSabun",
                table: "WorkApplyHistory");

            migrationBuilder.DropColumn(
                name: "ApprovalTel",
                table: "WorkApplyHistory");

            migrationBuilder.DropColumn(
                name: "ApprovalName",
                table: "WorkApply");

            migrationBuilder.DropColumn(
                name: "ApprovalOrgID",
                table: "WorkApply");

            migrationBuilder.DropColumn(
                name: "ApprovalOrgNameEng",
                table: "WorkApply");

            migrationBuilder.DropColumn(
                name: "ApprovalOrgNameKor",
                table: "WorkApply");

            migrationBuilder.DropColumn(
                name: "ApprovalSabun",
                table: "WorkApply");

            migrationBuilder.DropColumn(
                name: "ApprovalTel",
                table: "WorkApply");

            migrationBuilder.DropColumn(
                name: "ApprovalName",
                table: "CompanyHistory");

            migrationBuilder.DropColumn(
                name: "ApprovalOrgID",
                table: "CompanyHistory");

            migrationBuilder.DropColumn(
                name: "ApprovalOrgNameEng",
                table: "CompanyHistory");

            migrationBuilder.DropColumn(
                name: "ApprovalOrgNameKor",
                table: "CompanyHistory");

            migrationBuilder.DropColumn(
                name: "ApprovalSabun",
                table: "CompanyHistory");

            migrationBuilder.DropColumn(
                name: "ApprovalTel",
                table: "CompanyHistory");

            migrationBuilder.DropColumn(
                name: "ApprovalName",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "ApprovalOrgID",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "ApprovalOrgNameEng",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "ApprovalOrgNameKor",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "ApprovalSabun",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "ApprovalTel",
                table: "Company");
        }
    }
}
