using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebVisit.Migrations.DBSVISITDev
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Approval",
                columns: table => new
                {
                    ApprovalID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Location = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ApprovalType = table.Column<int>(type: "int", nullable: false),
                    InsertSabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    InsertName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InsertOrgID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InsertOrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    InsertOrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<string>(type: "char(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Approval", x => x.ApprovalID);
                });

            migrationBuilder.CreateTable(
                name: "ApprovalHistory",
                columns: table => new
                {
                    ApprovalHistoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApprovalID = table.Column<int>(type: "int", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ApprovalType = table.Column<int>(type: "int", nullable: false),
                    UpdateSabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UpdateName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateOrgID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateOrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdateOrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    InsertUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApprovalHistory", x => x.ApprovalHistoryID);
                });

            migrationBuilder.CreateTable(
                name: "ApprovalLine",
                columns: table => new
                {
                    ApprovalLineID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApprovalID = table.Column<int>(type: "int", nullable: false),
                    ApprovalSysType = table.Column<int>(type: "int", nullable: false),
                    ApprovalSabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ApprovalName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ApprovalOrgID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ApprovalOrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ApprovalOrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    InsertSabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    InsertName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InsertOrgID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InsertOrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    InsertOrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<string>(type: "char(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApprovalLine", x => x.ApprovalLineID);
                });

            migrationBuilder.CreateTable(
                name: "ApprovalLineHistory",
                columns: table => new
                {
                    ApprovalLineHistoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApprovalLineID = table.Column<int>(type: "int", nullable: false),
                    ApprovalID = table.Column<int>(type: "int", nullable: false),
                    ApprovalSysType = table.Column<int>(type: "int", nullable: false),
                    ApprovalSabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ApprovalName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ApprovalOrgID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ApprovalOrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ApprovalOrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdateSabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UpdateName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateOrgID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateOrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdateOrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    InsertUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApprovalLineHistory", x => x.ApprovalLineHistoryID);
                });

            migrationBuilder.CreateTable(
                name: "BlackList",
                columns: table => new
                {
                    BlackListID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VisitorType = table.Column<int>(type: "int", nullable: false),
                    VisitPersonID = table.Column<int>(type: "int", nullable: true),
                    Sabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OrgID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    OrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    BirthDate = table.Column<string>(type: "char(10)", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Tel = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    BlackListType = table.Column<int>(type: "int", nullable: false),
                    StatementFileData = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    StatementFileDataHash = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Reason = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    BlackListStatus = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InsertSabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    InsertName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InsertOrgID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InsertOrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    InsertOrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<string>(type: "char(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlackList", x => x.BlackListID);
                });

            migrationBuilder.CreateTable(
                name: "BlackListHistory",
                columns: table => new
                {
                    BlackListHIstoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BlackListID = table.Column<int>(type: "int", nullable: false),
                    VisitorType = table.Column<int>(type: "int", nullable: false),
                    VisitPersonID = table.Column<int>(type: "int", nullable: true),
                    Sabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OrgID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    OrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    BirthDate = table.Column<string>(type: "char(10)", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Tel = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    BlackListType = table.Column<int>(type: "int", nullable: false),
                    StatementFileData = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    StatementFileDataHash = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Reason = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    BlackListStatus = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Memo = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    UpdateSabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UpdateName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateOrgID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateOrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdateOrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    InsertUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlackListHistory", x => x.BlackListHIstoryID);
                });

            migrationBuilder.CreateTable(
                name: "CarCardIssueApply",
                columns: table => new
                {
                    CarCardIssueApplyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Location = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CompanyID = table.Column<int>(type: "int", nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PersonTypeID = table.Column<int>(type: "int", nullable: true),
                    Sabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OrgID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    OrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    GradeID = table.Column<int>(type: "int", nullable: true),
                    GradeName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Tel = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TermsPrivarcyFileData = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    TermsPrivarcyFileDataHash = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    ContactSabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ContactName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ContactOrgID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ContactOrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ContactOrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CardIssueType = table.Column<int>(type: "int", nullable: false),
                    ApplyReason = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    CarNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CarTypeCodeID = table.Column<int>(type: "int", nullable: false),
                    CarLIcenseFileData = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CarLIcenseFileDataHash = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    CarModel = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CardID = table.Column<int>(type: "int", nullable: true),
                    CardNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CardTypeID = table.Column<int>(type: "int", nullable: false),
                    CardName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CardApplyStatus = table.Column<int>(type: "int", nullable: false),
                    CardIssueStatus = table.Column<int>(type: "int", nullable: true),
                    CardValidDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReissueNo = table.Column<int>(type: "int", nullable: true),
                    InsertSabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    InsertName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InsertOrgID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InsertOrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    InsertOrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<string>(type: "char(1)", nullable: false),
                    LPRResult = table.Column<string>(type: "char(1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarCardIssueApply", x => x.CarCardIssueApplyID);
                });

            migrationBuilder.CreateTable(
                name: "CarCardIssueApplyHistory",
                columns: table => new
                {
                    CarCardIssueApplyHistoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarCardIssueApplyID = table.Column<int>(type: "int", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CompanyID = table.Column<int>(type: "int", nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PersonTypeID = table.Column<int>(type: "int", nullable: true),
                    Sabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OrgID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    OrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    GradeID = table.Column<int>(type: "int", nullable: true),
                    GradeName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Tel = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TermsPrivarcyFileData = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    TermsPrivarcyFileDataHash = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    ContactSabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ContactName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ContactOrgID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ContactOrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ContactOrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CardIssueType = table.Column<int>(type: "int", nullable: false),
                    ApplyReason = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    CarNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CarTypeCodeID = table.Column<int>(type: "int", nullable: false),
                    CarLIcenseFileData = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CarLIcenseFileDataHash = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    CarModel = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CardID = table.Column<int>(type: "int", nullable: true),
                    CardNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CardTypeID = table.Column<int>(type: "int", nullable: false),
                    CardName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CardApplyStatus = table.Column<int>(type: "int", nullable: false),
                    CardIssueStatus = table.Column<int>(type: "int", nullable: true),
                    CardValidDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReissueNo = table.Column<int>(type: "int", nullable: true),
                    Memo = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    UpdateSabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UpdateName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateOrgID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateOrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdateOrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    InsertUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarCardIssueApplyHistory", x => x.CarCardIssueApplyHistoryID);
                });

            migrationBuilder.CreateTable(
                name: "CardIssueApply",
                columns: table => new
                {
                    CardIssueApplyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Location = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CompanyID = table.Column<int>(type: "int", nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PersonTypeID = table.Column<int>(type: "int", nullable: true),
                    Sabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OrgID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    OrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    GradeID = table.Column<int>(type: "int", nullable: true),
                    GradeName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Tel = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TermsPrivarcyFileData = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    TermsPrivarcyFileDataHash = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    ContactSabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ContactName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ContactOrgID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ContactOrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ContactOrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CardIssueType = table.Column<int>(type: "int", nullable: false),
                    ApplyReason = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    CardID = table.Column<int>(type: "int", nullable: true),
                    CardNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CardTypeID = table.Column<int>(type: "int", nullable: false),
                    CardName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CardApplyStatus = table.Column<int>(type: "int", nullable: false),
                    CardIssueStatus = table.Column<int>(type: "int", nullable: true),
                    CardValidDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReissueNo = table.Column<int>(type: "int", nullable: true),
                    InsertSabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    InsertName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InsertOrgID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InsertOrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    InsertOrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<string>(type: "char(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardIssueApply", x => x.CardIssueApplyID);
                });

            migrationBuilder.CreateTable(
                name: "CardIssueApplyHistory",
                columns: table => new
                {
                    CardIssueApplyHistoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CardIssueApplyID = table.Column<int>(type: "int", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CompanyID = table.Column<int>(type: "int", nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PersonTypeID = table.Column<int>(type: "int", nullable: true),
                    Sabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OrgID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    OrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    GradeID = table.Column<int>(type: "int", nullable: true),
                    GradeName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Tel = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TermsPrivarcyFileData = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    TermsPrivarcyFileDataHash = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    ContactSabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ContactName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ContactOrgID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ContactOrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ContactOrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CardIssueType = table.Column<int>(type: "int", nullable: false),
                    ApplyReason = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    CardID = table.Column<int>(type: "int", nullable: true),
                    CardNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CardTypeID = table.Column<int>(type: "int", nullable: false),
                    CardName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CardApplyStatus = table.Column<int>(type: "int", nullable: false),
                    CardIssueStatus = table.Column<int>(type: "int", nullable: true),
                    CardValidDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReissueNo = table.Column<int>(type: "int", nullable: true),
                    Memo = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    UpdateSabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UpdateName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateOrgID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateOrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdateOrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    InsertUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardIssueApplyHistory", x => x.CardIssueApplyHistoryID);
                });

            migrationBuilder.CreateTable(
                name: "CarryItemApply",
                columns: table => new
                {
                    CarryItemApplyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImportDate = table.Column<string>(type: "char(10)", nullable: true),
                    ExportDate = table.Column<string>(type: "char(10)", nullable: true),
                    EntryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExitDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ImportPurposeCodeID = table.Column<int>(type: "int", nullable: false),
                    PlaceCodeID = table.Column<int>(type: "int", nullable: false),
                    ImportReason = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    ImportWayType = table.Column<int>(type: "int", nullable: false),
                    CarryVisitorType = table.Column<int>(type: "int", nullable: false),
                    VisitPersonID = table.Column<int>(type: "int", nullable: true),
                    Sabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OrgID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    OrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ApprovalSabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ApprovalName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ApprovalOrgID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ApprovalOrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ApprovalOrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ContactSabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ContactName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ContactOrgID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ContactOrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ContactOrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ContactMobile = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CarryItemApplyStatus = table.Column<int>(type: "int", nullable: false),
                    CarryItemStatus = table.Column<int>(type: "int", nullable: false),
                    CarryItemImportType = table.Column<int>(type: "int", nullable: true),
                    ApprovalOpinion = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    ApprovalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ImportTime = table.Column<string>(type: "char(5)", nullable: true),
                    ExportTime = table.Column<string>(type: "char(5)", nullable: true),
                    InsertVisitorType = table.Column<int>(type: "int", nullable: false),
                    InsertVisitPersonID = table.Column<int>(type: "int", nullable: true),
                    InsertSabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    InsertName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InsertOrgID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InsertOrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    InsertOrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<string>(type: "char(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarryItemApply", x => x.CarryItemApplyID);
                });

            migrationBuilder.CreateTable(
                name: "CarryItemApplyHistory",
                columns: table => new
                {
                    CarryItemApplyHistoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarryItemApplyID = table.Column<int>(type: "int", nullable: false),
                    ImportDate = table.Column<string>(type: "char(10)", nullable: true),
                    ExportDate = table.Column<string>(type: "char(10)", nullable: true),
                    EntryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExitDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ImportPurposeCodeID = table.Column<int>(type: "int", nullable: false),
                    PlaceCodeID = table.Column<int>(type: "int", nullable: false),
                    ImportReason = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    ImportWayType = table.Column<int>(type: "int", nullable: false),
                    CarryVisitorType = table.Column<int>(type: "int", nullable: false),
                    VisitPersonID = table.Column<int>(type: "int", nullable: true),
                    Sabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OrgID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    OrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ApprovalSabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ApprovalName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ApprovalOrgID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ApprovalOrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ApprovalOrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ContactSabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ContactName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ContactOrgID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ContactOrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ContactOrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ContactMobile = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CarryItemApplyStatus = table.Column<int>(type: "int", nullable: false),
                    CarryItemStatus = table.Column<int>(type: "int", nullable: false),
                    CarryItemImportType = table.Column<int>(type: "int", nullable: true),
                    ApprovalOpinion = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    ApprovalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Memo = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    ImportTime = table.Column<string>(type: "char(5)", nullable: true),
                    ExportTime = table.Column<string>(type: "char(5)", nullable: true),
                    UpdateVisitorType = table.Column<int>(type: "int", nullable: false),
                    UpdateVisitPersonID = table.Column<int>(type: "int", nullable: true),
                    UpdateSabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateOrgID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateOrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdateOrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    InsertUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarryItemApplyHistory", x => x.CarryItemApplyHistoryID);
                });

            migrationBuilder.CreateTable(
                name: "CarryItemInfo",
                columns: table => new
                {
                    CarryItemInfoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarryItemApplyID = table.Column<int>(type: "int", nullable: false),
                    CarryItemCodeID = table.Column<int>(type: "int", nullable: false),
                    ItemName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ItemSN = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    Unit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ExportCnt = table.Column<int>(type: "int", nullable: true),
                    RemainCnt = table.Column<int>(type: "int", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<string>(type: "char(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarryItemInfo", x => x.CarryItemInfoID);
                });

            migrationBuilder.CreateTable(
                name: "CarryItemInfoHistory",
                columns: table => new
                {
                    CarryItemInfoHistoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarryItemInfoID = table.Column<int>(type: "int", nullable: false),
                    CarryItemApplyID = table.Column<int>(type: "int", nullable: false),
                    CarryItemCodeID = table.Column<int>(type: "int", nullable: false),
                    ItemName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ItemSN = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    Unit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ExportCnt = table.Column<int>(type: "int", nullable: true),
                    RemainCnt = table.Column<int>(type: "int", nullable: true),
                    Memo = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    InsertUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarryItemInfoHistory", x => x.CarryItemInfoHistoryID);
                });

            migrationBuilder.CreateTable(
                name: "CommonCode",
                columns: table => new
                {
                    CommonCodeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupNo = table.Column<int>(type: "int", nullable: false),
                    SubCodeID = table.Column<int>(type: "int", nullable: true),
                    SubCodeDesc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CodeNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CodeNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    OrderNo = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<string>(type: "char(1)", nullable: false, defaultValue: "Y"),
                    IsSys = table.Column<string>(type: "char(1)", nullable: false, defaultValue: "N"),
                    Memo = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    InsertSabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    InsertName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InsertOrgID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InsertOrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    InsertOrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    IsDelete = table.Column<string>(type: "char(1)", nullable: false, defaultValue: "N")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommonCode", x => x.CommonCodeID);
                });

            migrationBuilder.CreateTable(
                name: "CommonCodeHistory",
                columns: table => new
                {
                    CommonCodeHistoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommonCodeID = table.Column<int>(type: "int", nullable: false),
                    GroupNo = table.Column<int>(type: "int", nullable: false),
                    CodeNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CodeNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SubCodeID = table.Column<int>(type: "int", nullable: true),
                    SubCodeDesc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OrderNo = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<string>(type: "char(1)", nullable: false),
                    IsSys = table.Column<string>(type: "char(1)", nullable: false),
                    Memo = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    UpdateSabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UpdateName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateOrgID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateOrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdateOrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    InsertUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommonCodeHistory", x => x.CommonCodeHistoryID);
                });

            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    CompanyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyType = table.Column<int>(type: "int", nullable: false),
                    BizRegNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CeoName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Tel = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    BizFileData = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    BizFileDataHash = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    ContactPersonName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ContactPersonTel = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CompanyStatus = table.Column<int>(type: "int", nullable: false),
                    InsertSabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    InsertName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InsertOrgID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InsertOrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    InsertOrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<string>(type: "char(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.CompanyID);
                });

            migrationBuilder.CreateTable(
                name: "CompanyHistory",
                columns: table => new
                {
                    CompanyHistoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyID = table.Column<int>(type: "int", nullable: false),
                    CompanyType = table.Column<int>(type: "int", nullable: false),
                    BizRegNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CeoName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Tel = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    BizFileData = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    BizFileDataHash = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    ContactPersonName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ContactPersonTel = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CompanyStatus = table.Column<int>(type: "int", nullable: false),
                    UpdateSabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UpdateName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateOrgID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateOrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdateOrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    InsertUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyHistory", x => x.CompanyHistoryID);
                });

            migrationBuilder.CreateTable(
                name: "ExportImport",
                columns: table => new
                {
                    ExportImportID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExportImportNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ExportImportType = table.Column<int>(type: "int", nullable: false),
                    ExportType = table.Column<int>(type: "int", nullable: false),
                    ExportLocation = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ImportLocation = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ExportImportPurposeType = table.Column<int>(type: "int", nullable: true),
                    ImportType = table.Column<int>(type: "int", nullable: true),
                    ImportDate = table.Column<string>(type: "char(10)", nullable: true),
                    ExportDate = table.Column<string>(type: "char(10)", nullable: true),
                    DeliveryMethodType = table.Column<int>(type: "int", nullable: true),
                    Reason = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    IsVMI = table.Column<string>(type: "char(1)", nullable: true),
                    CompanySabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CompanyContactName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    BizRegNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CompanyTel = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsSelf = table.Column<string>(type: "char(1)", nullable: true),
                    Sabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OrgID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    OrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Tel = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ApprovalSabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ApprovalName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ApprovalOrgID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ApprovalOrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ApprovalOrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ApprovalTel = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ContactSabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ContactName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ContactOrgID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ContactOrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ContactOrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ContactTel = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ExportImportStatus = table.Column<int>(type: "int", nullable: false),
                    ExportDateHour = table.Column<string>(type: "char(2)", nullable: true),
                    ExportDateMinute = table.Column<string>(type: "char(2)", nullable: true),
                    ImportDateHour = table.Column<string>(type: "char(2)", nullable: true),
                    ImportDateMinute = table.Column<string>(type: "char(2)", nullable: true),
                    ManagementNo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SerialNo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsSelfApproval = table.Column<string>(type: "char(1)", nullable: true),
                    CeoApprovalFileData = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CeoApprovalFileDataHash = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    InsertSabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    InsertName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InsertOrgID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InsertOrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    InsertOrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<string>(type: "char(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExportImport", x => x.ExportImportID);
                });

            migrationBuilder.CreateTable(
                name: "ExportImportHistory",
                columns: table => new
                {
                    ExportImportHistoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExportImportID = table.Column<int>(type: "int", nullable: false),
                    ExportImportNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ExportImportType = table.Column<int>(type: "int", nullable: false),
                    ExportType = table.Column<int>(type: "int", nullable: false),
                    ExportLocation = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ImportLocation = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ExportImportPurposeType = table.Column<int>(type: "int", nullable: true),
                    ImportType = table.Column<int>(type: "int", nullable: true),
                    ImportDate = table.Column<string>(type: "char(10)", nullable: true),
                    ExportDate = table.Column<string>(type: "char(10)", nullable: true),
                    DeliveryMethodType = table.Column<int>(type: "int", nullable: true),
                    Reason = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    IsVMI = table.Column<string>(type: "char(1)", nullable: true),
                    CompanySabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CompanyContactName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    BizRegNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CompanyTel = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsSelf = table.Column<string>(type: "char(1)", nullable: true),
                    Sabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OrgID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    OrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Tel = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ApprovalSabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ApprovalName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ApprovalOrgID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ApprovalOrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ApprovalOrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ApprovalTel = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ContactSabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ContactName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ContactOrgID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ContactOrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ContactOrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ContactTel = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ExportImportStatus = table.Column<int>(type: "int", nullable: false),
                    ExportDateHour = table.Column<string>(type: "char(2)", nullable: true),
                    ExportDateMinute = table.Column<string>(type: "char(2)", nullable: true),
                    ImportDateHour = table.Column<string>(type: "char(2)", nullable: true),
                    ImportDateMinute = table.Column<string>(type: "char(2)", nullable: true),
                    ManagementNo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SerialNo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsSelfApproval = table.Column<string>(type: "char(1)", nullable: true),
                    CeoApprovalFileData = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CeoApprovalFileDataHash = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Opinion = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    CheckCnt = table.Column<int>(type: "int", nullable: true),
                    UpdateSabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UpdateName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateOrgID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateOrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdateOrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdateGradeName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    InsertUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExportImportHistory", x => x.ExportImportHistoryID);
                });

            migrationBuilder.CreateTable(
                name: "ExportImportItem",
                columns: table => new
                {
                    ExportImportItemID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExportImportID = table.Column<int>(type: "int", nullable: false),
                    PRNo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    MaterialsCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    MaterialsName = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    Unit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Standard = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Memo = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<string>(type: "char(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExportImportItem", x => x.ExportImportItemID);
                });

            migrationBuilder.CreateTable(
                name: "Holiday",
                columns: table => new
                {
                    HolidayID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HolidayDate = table.Column<string>(type: "char(10)", nullable: false),
                    HolidayName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsManual = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false, defaultValue: "N"),
                    InsertSabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    InsertName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InsertOrgID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InsertOrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    InsertOrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    IsDelete = table.Column<string>(type: "char(1)", nullable: false, defaultValue: "N")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Holiday", x => x.HolidayID);
                });

            migrationBuilder.CreateTable(
                name: "Level",
                columns: table => new
                {
                    LevelID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LevelName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    InsertSabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    InsertName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InsertOrgID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InsertOrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    InsertOrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    IsDelete = table.Column<string>(type: "char(1)", nullable: false, defaultValue: "N")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Level", x => x.LevelID);
                });

            migrationBuilder.CreateTable(
                name: "LevelHistory",
                columns: table => new
                {
                    LevelHistoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LevelID = table.Column<int>(type: "int", nullable: false),
                    LevelName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UpdateSabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UpdateName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateOrgID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateOrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdateOrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    InsertUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LevelHistory", x => x.LevelHistoryID);
                });

            migrationBuilder.CreateTable(
                name: "Material",
                columns: table => new
                {
                    MaterialID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DIV_CODE = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    STOCK_CODE = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    STOCK_NM = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    STOCK_RULE = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    STOCK_UNIT = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    W_DATE = table.Column<string>(type: "char(10)", nullable: true),
                    IsDelete = table.Column<string>(type: "char(1)", nullable: false, defaultValue: "N")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Material", x => x.MaterialID);
                });

            migrationBuilder.CreateTable(
                name: "MealAccess",
                columns: table => new
                {
                    MealAccessID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MealGB1 = table.Column<int>(type: "int", nullable: true),
                    MealGB2 = table.Column<int>(type: "int", nullable: true),
                    MealGB3 = table.Column<int>(type: "int", nullable: true),
                    MealGB4 = table.Column<int>(type: "int", nullable: true),
                    MealGB5 = table.Column<int>(type: "int", nullable: true),
                    MealGB6 = table.Column<int>(type: "int", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealAccess", x => x.MealAccessID);
                });

            migrationBuilder.CreateTable(
                name: "MealLog",
                columns: table => new
                {
                    MealLogID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    OrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    GradeName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    MealYMD = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    MealGB = table.Column<int>(type: "int", nullable: true),
                    Price = table.Column<int>(type: "int", nullable: true),
                    EqMasterID = table.Column<int>(type: "int", nullable: true),
                    IsManual = table.Column<int>(type: "int", nullable: false),
                    IsVisitor = table.Column<int>(type: "int", nullable: false),
                    VisitantName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    VisitantCompany = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    VisitantGrade = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    UpdateIP = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    StateName = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    CompanyID = table.Column<int>(type: "int", nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UpdateSabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealLog", x => x.MealLogID);
                });

            migrationBuilder.CreateTable(
                name: "MealPrice",
                columns: table => new
                {
                    MealPriceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Location = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Price = table.Column<int>(type: "int", nullable: false),
                    UpdateIP = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateSabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InsertName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InsertOrgID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InsertOrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    InsertOrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    IsDelete = table.Column<string>(type: "char(1)", nullable: false, defaultValue: "N")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealPrice", x => x.MealPriceID);
                });

            migrationBuilder.CreateTable(
                name: "MealSchedule",
                columns: table => new
                {
                    MealScheduleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Term = table.Column<int>(type: "int", nullable: false),
                    MealGB = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<string>(type: "char(4)", nullable: false),
                    EndTime = table.Column<string>(type: "char(4)", nullable: false),
                    UpdateIP = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateSabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InsertName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InsertOrgID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InsertOrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    InsertOrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    IsDelete = table.Column<string>(type: "char(1)", nullable: false, defaultValue: "N")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealSchedule", x => x.MealScheduleID);
                });

            migrationBuilder.CreateTable(
                name: "MealTerm",
                columns: table => new
                {
                    MealTermID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Term = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<string>(type: "varchar(4)", nullable: false),
                    EndDate = table.Column<string>(type: "varchar(4)", nullable: false),
                    UpdateIP = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateSabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InsertName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InsertOrgID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InsertOrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    InsertOrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    IsDelete = table.Column<string>(type: "char(1)", nullable: false, defaultValue: "N")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealTerm", x => x.MealTermID);
                });

            migrationBuilder.CreateTable(
                name: "Menu",
                columns: table => new
                {
                    MenuID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupNo = table.Column<int>(type: "int", nullable: false),
                    Depth = table.Column<int>(type: "int", nullable: false),
                    OrderNo = table.Column<int>(type: "int", nullable: false),
                    MenuNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MenuNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    URL = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IconImg = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsDisplay = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false, defaultValue: "Y"),
                    InsertSabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    InsertName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InsertOrgID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InsertOrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    InsertOrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    IsDelete = table.Column<string>(type: "char(1)", nullable: false, defaultValue: "N")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menu", x => x.MenuID);
                });

            migrationBuilder.CreateTable(
                name: "MenuHistory",
                columns: table => new
                {
                    MenuHistoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MenuID = table.Column<int>(type: "int", nullable: false),
                    GroupNo = table.Column<int>(type: "int", nullable: false),
                    Depth = table.Column<int>(type: "int", nullable: false),
                    OrderNo = table.Column<int>(type: "int", nullable: false),
                    MenuNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MenuNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    URL = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IconImg = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsDisplay = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    UpdateSabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UpdateName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateOrgID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateOrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdateOrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    InsertUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuHistory", x => x.MenuHistoryID);
                });

            migrationBuilder.CreateTable(
                name: "MenuLevel",
                columns: table => new
                {
                    MenuLevelID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MenuID = table.Column<int>(type: "int", nullable: false),
                    LevelID = table.Column<int>(type: "int", nullable: false),
                    MenuNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MenuNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsAccess = table.Column<string>(type: "char(1)", nullable: false, defaultValue: "Y"),
                    IsDisplay = table.Column<string>(type: "char(1)", nullable: false, defaultValue: "N"),
                    InsertSabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    InsertName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InsertOrgID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InsertOrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    InsertOrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    IsDelete = table.Column<string>(type: "char(1)", nullable: false, defaultValue: "N")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuLevel", x => x.MenuLevelID);
                });

            migrationBuilder.CreateTable(
                name: "MenuLevelHistory",
                columns: table => new
                {
                    MenuLevelHistoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MenuLevelID = table.Column<int>(type: "int", nullable: false),
                    MenuID = table.Column<int>(type: "int", nullable: false),
                    LevelID = table.Column<int>(type: "int", nullable: false),
                    MenuNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MenuNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsAccess = table.Column<string>(type: "char(1)", nullable: false),
                    IsDisplay = table.Column<string>(type: "char(1)", nullable: false),
                    UpdateSabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UpdateName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateOrgID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateOrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdateOrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    InsertUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuLevelHistory", x => x.MenuLevelHistoryID);
                });

            migrationBuilder.CreateTable(
                name: "NotebookSelfApproval",
                columns: table => new
                {
                    NotebookSelfApprovalID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Location = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Sabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OrgID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    OrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    GradeID = table.Column<int>(type: "int", nullable: true),
                    GradeName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    InsertSabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    InsertName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InsertOrgID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InsertOrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    InsertOrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<string>(type: "char(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotebookSelfApproval", x => x.NotebookSelfApprovalID);
                });

            migrationBuilder.CreateTable(
                name: "NotebookSelfApprovalHistory",
                columns: table => new
                {
                    NotebookSelfApprovalHistoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NotebookSelfApprovalID = table.Column<int>(type: "int", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Sabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OrgID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    OrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    GradeID = table.Column<int>(type: "int", nullable: true),
                    GradeName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdateSabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UpdateName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateOrgID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateOrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdateOrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    InsertUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotebookSelfApprovalHistory", x => x.NotebookSelfApprovalHistoryID);
                });

            migrationBuilder.CreateTable(
                name: "Notice",
                columns: table => new
                {
                    NoticeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Contents = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsPublish = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    FileData1 = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    FileDataHash1 = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    FileData2 = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    FileDataHash2 = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    InsertSabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    InsertName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InsertOrgID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InsertOrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    InsertOrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<string>(type: "char(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notice", x => x.NoticeID);
                });

            migrationBuilder.CreateTable(
                name: "PasswordHistory",
                columns: table => new
                {
                    PasswordHistoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OrgID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    OrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ChangeType = table.Column<int>(type: "int", nullable: false),
                    UpdateSabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UpdateName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateOrgID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateOrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdateOrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    InsertUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PasswordHistory", x => x.PasswordHistoryID);
                });

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    PersonID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PID = table.Column<int>(type: "int", nullable: true),
                    Sabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CompanyID = table.Column<int>(type: "int", nullable: true),
                    PersonTypeID = table.Column<int>(type: "int", nullable: true),
                    OrgID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    OrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LevelCodeID = table.Column<int>(type: "int", nullable: false),
                    GradeID = table.Column<int>(type: "int", nullable: true),
                    GradeName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Password = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    PWUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BirthDate = table.Column<string>(type: "char(10)", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    Mobile = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Tel = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ImageData = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ImageDataHash = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PersonStatusID = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    HomeAddr = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    HomeDetailAddr = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    HomeZipcode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    IsForeigner = table.Column<int>(type: "int", nullable: true, defaultValue: 1),
                    Nationality = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ImmStatusCodeID = table.Column<int>(type: "int", nullable: true),
                    ImmStartDate = table.Column<string>(type: "char(10)", nullable: true),
                    ImmEndDate = table.Column<string>(type: "char(10)", nullable: true),
                    IsAllowSMS = table.Column<string>(type: "char(1)", nullable: false, defaultValue: "N"),
                    WorkTypeCodeID = table.Column<int>(type: "int", nullable: true),
                    CarAllowCnt = table.Column<int>(type: "int", nullable: true),
                    CarRegCnt = table.Column<int>(type: "int", nullable: true),
                    TermsPrivarcyFileData = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    TermsPrivarcyFileDataHash = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    CardIssueStatus = table.Column<int>(type: "int", nullable: true),
                    CardCreateCnt = table.Column<int>(type: "int", nullable: true),
                    VisitorEduLastDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CleanEduLastDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SafetyEduLastDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VisitLastDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateIP = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CardID = table.Column<int>(type: "int", nullable: true),
                    CardNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsRecTempCard = table.Column<string>(type: "char(1)", nullable: true),
                    TempCardRecDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InsertSabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    InsertName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InsertOrgID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InsertOrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    InsertOrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    IsDelete = table.Column<string>(type: "char(1)", nullable: false, defaultValue: "N")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.PersonID);
                });

            migrationBuilder.CreateTable(
                name: "SafetyEdu",
                columns: table => new
                {
                    SafetyEduID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EduDate = table.Column<string>(type: "char(10)", nullable: false),
                    WorkApplyID = table.Column<int>(type: "int", nullable: true),
                    EduApplyStatus = table.Column<int>(type: "int", nullable: false),
                    InsertSabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    InsertName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InsertOrgID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InsertOrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    InsertOrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<string>(type: "char(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SafetyEdu", x => x.SafetyEduID);
                });

            migrationBuilder.CreateTable(
                name: "SafetyEduApply",
                columns: table => new
                {
                    SafetyEduApplyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SafetyEduID = table.Column<int>(type: "int", nullable: false),
                    EduCompleteStatus = table.Column<int>(type: "int", nullable: false),
                    EduDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Sabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OrgID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    OrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<string>(type: "char(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SafetyEduApply", x => x.SafetyEduApplyID);
                });

            migrationBuilder.CreateTable(
                name: "SafetyEduApplyHistory",
                columns: table => new
                {
                    SafetyEduApplyHistoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SafetyEduApplyID = table.Column<int>(type: "int", nullable: false),
                    SafetyEduID = table.Column<int>(type: "int", nullable: false),
                    EduCompleteStatus = table.Column<int>(type: "int", nullable: false),
                    EduDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ValidDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Sabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OrgID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    OrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    InsertUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SafetyEduApplyHistory", x => x.SafetyEduApplyHistoryID);
                });

            migrationBuilder.CreateTable(
                name: "SafetyEduHistory",
                columns: table => new
                {
                    SafetyEduHistoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SafetyEduID = table.Column<int>(type: "int", nullable: false),
                    EduDate = table.Column<string>(type: "char(10)", nullable: false),
                    WorkApplyID = table.Column<int>(type: "int", nullable: true),
                    EduApplyStatus = table.Column<int>(type: "int", nullable: false),
                    UpdateSabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UpdateName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateOrgID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateOrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdateOrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    InsertUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SafetyEduHistory", x => x.SafetyEduHistoryID);
                });

            migrationBuilder.CreateTable(
                name: "SafetyViolation",
                columns: table => new
                {
                    SafetyViolationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ViolationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WorkOrderNo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsWorkOrder = table.Column<string>(type: "char(1)", nullable: false),
                    ContactSabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ContactName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ContactOrgID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ContactOrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ContactOrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Location = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ViolationLocation = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SafetyViolationReasonID = table.Column<int>(type: "int", nullable: false),
                    DetailReason = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    SafetyViolationStatus = table.Column<int>(type: "int", nullable: false),
                    StatementFileData = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    StatementFileDataHash = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    InsertSabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    InsertName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InsertOrgID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InsertOrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    InsertOrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<string>(type: "char(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SafetyViolation", x => x.SafetyViolationID);
                });

            migrationBuilder.CreateTable(
                name: "SafetyViolationHistory",
                columns: table => new
                {
                    SafetyViolationHistoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SafetyViolationID = table.Column<int>(type: "int", nullable: false),
                    ViolationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WorkOrderNo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsWorkOrder = table.Column<string>(type: "char(1)", nullable: false),
                    ContactSabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ContactName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ContactOrgID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ContactOrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ContactOrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Location = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ViolationLocation = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SafetyViolationReasonID = table.Column<int>(type: "int", nullable: false),
                    DetailReason = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    SafetyViolationStatus = table.Column<int>(type: "int", nullable: false),
                    StatementFileData = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    StatementFileDataHash = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    UpdateSabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UpdateName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateOrgID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateOrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdateOrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    InsertUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SafetyViolationHistory", x => x.SafetyViolationHistoryID);
                });

            migrationBuilder.CreateTable(
                name: "SafetyViolationPerson",
                columns: table => new
                {
                    SafetyViolationPersonID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SafetyViolationID = table.Column<int>(type: "int", nullable: false),
                    SafetyViolationReasonID = table.Column<int>(type: "int", nullable: false),
                    Sabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OrgID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    OrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    BirthDate = table.Column<string>(type: "char(10)", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ViolationTime = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<string>(type: "char(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SafetyViolationPerson", x => x.SafetyViolationPersonID);
                });

            migrationBuilder.CreateTable(
                name: "SafetyViolationPersonHistory",
                columns: table => new
                {
                    SafetyViolationPersonHistoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SafetyViolationPersonID = table.Column<int>(type: "int", nullable: false),
                    SafetyViolationID = table.Column<int>(type: "int", nullable: false),
                    SafetyViolationReasonID = table.Column<int>(type: "int", nullable: false),
                    Sabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OrgID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    OrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    BirthDate = table.Column<string>(type: "char(10)", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ViolationTime = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InsertUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SafetyViolationPersonHistory", x => x.SafetyViolationPersonHistoryID);
                });

            migrationBuilder.CreateTable(
                name: "SafetyViolationReason",
                columns: table => new
                {
                    SafetyViolationReasonID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SafetyViolationReasonContents = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    ViolationPenaltyPeoriod1 = table.Column<int>(type: "int", nullable: false),
                    ViolationPenaltyPeoriod2 = table.Column<int>(type: "int", nullable: true),
                    ViolationPenaltyPeoriod3 = table.Column<int>(type: "int", nullable: true),
                    ViolationLevel = table.Column<int>(type: "int", nullable: true),
                    OrderNo = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<string>(type: "char(1)", nullable: false),
                    InsertSabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    InsertName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InsertOrgID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InsertOrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    InsertOrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdateSabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateOrgID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateOrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdateOrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<string>(type: "char(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SafetyViolationReason", x => x.SafetyViolationReasonID);
                });

            migrationBuilder.CreateTable(
                name: "SafetyViolationReasonHistory",
                columns: table => new
                {
                    SafetyViolationReasonHistoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SafetyViolationReasonID = table.Column<int>(type: "int", nullable: false),
                    SafetyViolationReasonContents = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    ViolationPenaltyPeoriod1 = table.Column<int>(type: "int", nullable: false),
                    ViolationPenaltyPeoriod2 = table.Column<int>(type: "int", nullable: true),
                    ViolationPenaltyPeoriod3 = table.Column<int>(type: "int", nullable: true),
                    ViolationLevel = table.Column<int>(type: "int", nullable: true),
                    OrderNo = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<string>(type: "char(1)", nullable: false),
                    UpdateSabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UpdateName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateOrgID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateOrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdateOrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    InsertUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SafetyViolationReasonHistory", x => x.SafetyViolationReasonHistoryID);
                });

            migrationBuilder.CreateTable(
                name: "SMS",
                columns: table => new
                {
                    SMSID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SMSType = table.Column<int>(type: "int", nullable: false),
                    VisitorType = table.Column<int>(type: "int", nullable: false),
                    VisitPersonID = table.Column<int>(type: "int", nullable: true),
                    Sabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OrgID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    OrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SendDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InsertSabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    InsertName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InsertOrgID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InsertOrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    InsertOrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<string>(type: "char(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SMS", x => x.SMSID);
                });

            migrationBuilder.CreateTable(
                name: "TempCardIssueApply",
                columns: table => new
                {
                    TempCardIssueApplyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Location = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CompanyID = table.Column<int>(type: "int", nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PersonTypeID = table.Column<int>(type: "int", nullable: true),
                    Sabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OrgID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    OrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    GradeID = table.Column<int>(type: "int", nullable: true),
                    GradeName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Tel = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CardID = table.Column<int>(type: "int", nullable: true),
                    CardNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CardTypeID = table.Column<int>(type: "int", nullable: false),
                    CardName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TempCardIssueStatus = table.Column<int>(type: "int", nullable: false),
                    ReturnDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CardValidDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReissueNo = table.Column<int>(type: "int", nullable: true),
                    InsertSabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    InsertName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InsertOrgID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InsertOrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    InsertOrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<string>(type: "char(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempCardIssueApply", x => x.TempCardIssueApplyID);
                });

            migrationBuilder.CreateTable(
                name: "TempCardIssueApplyHistory",
                columns: table => new
                {
                    TempCardIssueApplyHistoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TempCardIssueApplyID = table.Column<int>(type: "int", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CompanyID = table.Column<int>(type: "int", nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PersonTypeID = table.Column<int>(type: "int", nullable: true),
                    Sabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OrgID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    OrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    GradeID = table.Column<int>(type: "int", nullable: true),
                    GradeName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Tel = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CardID = table.Column<int>(type: "int", nullable: true),
                    CardNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CardTypeID = table.Column<int>(type: "int", nullable: false),
                    CardName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TempCardIssueStatus = table.Column<int>(type: "int", nullable: false),
                    ReturnDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CardValidDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReissueNo = table.Column<int>(type: "int", nullable: true),
                    UpdateSabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UpdateName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateOrgID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateOrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdateOrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    InsertUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempCardIssueApplyHistory", x => x.TempCardIssueApplyHistoryID);
                });

            migrationBuilder.CreateTable(
                name: "VisitApply",
                columns: table => new
                {
                    VisitApplyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Location = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    VisitStartDate = table.Column<string>(type: "char(10)", nullable: false),
                    VisitEndDate = table.Column<string>(type: "char(10)", nullable: false),
                    PlaceCodeID = table.Column<int>(type: "int", nullable: false),
                    VisitPurposeCodeID = table.Column<int>(type: "int", nullable: true),
                    VisitManualPurposeCodeID = table.Column<int>(type: "int", nullable: true),
                    ContactSabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ContactName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ContactOrgID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ContactOrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ContactOrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    VisitApplyType = table.Column<int>(type: "int", nullable: false),
                    WorkApplyID = table.Column<int>(type: "int", nullable: true),
                    WorkVisitApplyID = table.Column<int>(type: "int", nullable: true),
                    SafetyEduID = table.Column<int>(type: "int", nullable: true),
                    VisitApplyStatus = table.Column<int>(type: "int", nullable: false),
                    Purpose = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    RegVisitorType = table.Column<int>(type: "int", nullable: false),
                    VisitPersonID = table.Column<int>(type: "int", nullable: true),
                    InsertSabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    InsertName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InsertOrgID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InsertOrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    InsertOrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<string>(type: "char(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisitApply", x => x.VisitApplyID);
                });

            migrationBuilder.CreateTable(
                name: "VisitApplyHistory",
                columns: table => new
                {
                    VisitApplyHistoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VisitApplyID = table.Column<int>(type: "int", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    VisitStartDate = table.Column<string>(type: "char(10)", nullable: false),
                    VisitEndDate = table.Column<string>(type: "char(10)", nullable: false),
                    PlaceCodeID = table.Column<int>(type: "int", nullable: false),
                    VisitPurposeCodeID = table.Column<int>(type: "int", nullable: true),
                    VisitManualPurposeCodeID = table.Column<int>(type: "int", nullable: true),
                    ContactSabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ContactName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ContactOrgID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ContactOrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ContactOrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    VisitApplyType = table.Column<int>(type: "int", nullable: false),
                    WorkApplyID = table.Column<int>(type: "int", nullable: true),
                    WorkVisitApplyID = table.Column<int>(type: "int", nullable: true),
                    SafetyEduID = table.Column<int>(type: "int", nullable: true),
                    VisitApplyStatus = table.Column<int>(type: "int", nullable: false),
                    Purpose = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    RegVisitorType = table.Column<int>(type: "int", nullable: false),
                    VisitPersonID = table.Column<int>(type: "int", nullable: true),
                    RejectReason = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    UpdateSabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateOrgID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateOrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdateOrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    InsertUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisitApplyHistory", x => x.VisitApplyHistoryID);
                });

            migrationBuilder.CreateTable(
                name: "VisitApplyPerson",
                columns: table => new
                {
                    VisitApplyPersonID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VisitApplyID = table.Column<int>(type: "int", nullable: false),
                    VisitDate = table.Column<string>(type: "char(10)", nullable: false),
                    VisitorType = table.Column<int>(type: "int", nullable: false),
                    VisitPersonID = table.Column<int>(type: "int", nullable: true),
                    BirthDate = table.Column<string>(type: "char(10)", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Sabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OrgID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    OrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    OrderNo = table.Column<int>(type: "int", nullable: false),
                    WorkApplyID = table.Column<int>(type: "int", nullable: true),
                    WorkVisitApplyID = table.Column<int>(type: "int", nullable: true),
                    SafetyEduID = table.Column<int>(type: "int", nullable: true),
                    IsVisitorEdu = table.Column<string>(type: "char(1)", nullable: true),
                    VisitorEduDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsCleanEdu = table.Column<string>(type: "char(1)", nullable: true),
                    CleanEduDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CleanEduScore = table.Column<int>(type: "int", nullable: true),
                    IsSafetyEdu = table.Column<string>(type: "char(1)", nullable: true),
                    SafetyEduDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CarNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsTermsPrivarcy = table.Column<string>(type: "char(1)", nullable: true),
                    VisitApplyStatus = table.Column<int>(type: "int", nullable: false),
                    VisitStatus = table.Column<int>(type: "int", nullable: false),
                    CardID = table.Column<int>(type: "int", nullable: true),
                    CardNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsVIP = table.Column<string>(type: "char(1)", nullable: true),
                    VipTypeCodeID = table.Column<int>(type: "int", nullable: true),
                    EntryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExitDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CleanEntryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CleanExitDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InsertVisitorType = table.Column<int>(type: "int", nullable: false),
                    InsertVisitPersonID = table.Column<int>(type: "int", nullable: true),
                    InsertSabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    InsertName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InsertOrgID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InsertOrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    InsertOrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<string>(type: "char(1)", nullable: false),
                    LPRResult = table.Column<string>(type: "char(1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisitApplyPerson", x => x.VisitApplyPersonID);
                });

            migrationBuilder.CreateTable(
                name: "VisitApplyPersonHistory",
                columns: table => new
                {
                    VisitApplyPersonHistoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VisitApplyPersonID = table.Column<int>(type: "int", nullable: false),
                    VisitApplyID = table.Column<int>(type: "int", nullable: false),
                    VisitDate = table.Column<string>(type: "char(10)", nullable: false),
                    VisitorType = table.Column<int>(type: "int", nullable: false),
                    VisitPersonID = table.Column<int>(type: "int", nullable: true),
                    BirthDate = table.Column<string>(type: "char(10)", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Sabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OrgID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    OrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    OrderNo = table.Column<int>(type: "int", nullable: false),
                    WorkApplyID = table.Column<int>(type: "int", nullable: true),
                    WorkVisitApplyID = table.Column<int>(type: "int", nullable: true),
                    SafetyEduID = table.Column<int>(type: "int", nullable: true),
                    IsVisitorEdu = table.Column<string>(type: "char(1)", nullable: true),
                    VisitorEduDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsCleanEdu = table.Column<string>(type: "char(1)", nullable: true),
                    CleanEduDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CleanEduScore = table.Column<int>(type: "int", nullable: true),
                    IsSafetyEdu = table.Column<string>(type: "char(1)", nullable: true),
                    SafetyEduDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CarNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsTermsPrivarcy = table.Column<string>(type: "char(1)", nullable: true),
                    VisitApplyStatus = table.Column<int>(type: "int", nullable: false),
                    VisitStatus = table.Column<int>(type: "int", nullable: false),
                    CardID = table.Column<int>(type: "int", nullable: true),
                    CardNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsVIP = table.Column<string>(type: "char(1)", nullable: true),
                    VipTypeCodeID = table.Column<int>(type: "int", nullable: true),
                    EntryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExitDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CleanEntryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CleanExitDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateVisitorType = table.Column<int>(type: "int", nullable: false),
                    UpdateVisitPersonID = table.Column<int>(type: "int", nullable: true),
                    UpdateSabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateOrgID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateOrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdateOrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    InsertUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisitApplyPersonHistory", x => x.VisitApplyPersonHistoryID);
                });

            migrationBuilder.CreateTable(
                name: "VisitPerson",
                columns: table => new
                {
                    VisitPersonID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BirthDate = table.Column<string>(type: "char(10)", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    GradeName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CarNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Tel = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    VisitorEduLastDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CleanEduLastDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SafetyEduLastDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VisitLastDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RegVisitorType = table.Column<int>(type: "int", nullable: false),
                    InsertSabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    InsertName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InsertOrgID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InsertOrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    InsertOrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisitPerson", x => x.VisitPersonID);
                });

            migrationBuilder.CreateTable(
                name: "WorkApply",
                columns: table => new
                {
                    WorkApplyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkApplyNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ContactSabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ContactName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ContactOrgID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ContactOrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ContactOrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    WorkName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    WorkMemo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    WorkStartDate = table.Column<string>(type: "char(10)", nullable: false),
                    WorkEndDate = table.Column<string>(type: "char(10)", nullable: false),
                    WorkContractFileData = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    WorkContractFileDataHash = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    WorkApplyStatus = table.Column<int>(type: "int", nullable: false),
                    IsWorkReg = table.Column<string>(type: "char(1)", nullable: false),
                    InsertSabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    InsertName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InsertOrgID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InsertOrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    InsertOrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<string>(type: "char(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkApply", x => x.WorkApplyID);
                });

            migrationBuilder.CreateTable(
                name: "WorkApplyAttachFile",
                columns: table => new
                {
                    WorkApplyAttachFileID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkApplyID = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    AttachFileData = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    AttachFileDataHash = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<string>(type: "char(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkApplyAttachFile", x => x.WorkApplyAttachFileID);
                });

            migrationBuilder.CreateTable(
                name: "WorkApplyHistory",
                columns: table => new
                {
                    WorkApplyHistoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkApplyID = table.Column<int>(type: "int", nullable: false),
                    WorkApplyNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ContactSabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ContactName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ContactOrgID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ContactOrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ContactOrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    WorkName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    WorkMemo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    WorkStartDate = table.Column<string>(type: "char(10)", nullable: false),
                    WorkEndDate = table.Column<string>(type: "char(10)", nullable: false),
                    WorkContractFileData = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    WorkContractFileDataHash = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    WorkApplyStatus = table.Column<int>(type: "int", nullable: false),
                    IsWorkReg = table.Column<string>(type: "char(1)", nullable: false),
                    RejectReason = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    UpdateSabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UpdateName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateOrgID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateOrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdateOrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    InsertUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkApplyHistory", x => x.WorkApplyHistoryID);
                });

            migrationBuilder.CreateTable(
                name: "WorkVisitApply",
                columns: table => new
                {
                    WorkVisitApplyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkApplyID = table.Column<int>(type: "int", nullable: false),
                    WorkDate = table.Column<string>(type: "char(10)", nullable: false),
                    WorkVisitApplyStatus = table.Column<int>(type: "int", nullable: false),
                    InsertSabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    InsertName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InsertOrgID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InsertOrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    InsertOrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<string>(type: "char(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkVisitApply", x => x.WorkVisitApplyID);
                });

            migrationBuilder.CreateTable(
                name: "WorkVisitApplyHistory",
                columns: table => new
                {
                    WorkVisitApplyHistoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkVisitApplyID = table.Column<int>(type: "int", nullable: false),
                    WorkApplyID = table.Column<int>(type: "int", nullable: false),
                    WorkDate = table.Column<string>(type: "char(10)", nullable: false),
                    WorkVisitApplyStatus = table.Column<int>(type: "int", nullable: false),
                    UpdateSabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UpdateName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateOrgID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateOrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdateOrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    InsertUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkVisitApplyHistory", x => x.WorkVisitApplyHistoryID);
                });

            migrationBuilder.CreateTable(
                name: "WorkVisitPersonApply",
                columns: table => new
                {
                    WorkVisitPersonApplyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkVisitApplyID = table.Column<int>(type: "int", nullable: false),
                    WorkVisitApplyStatus = table.Column<int>(type: "int", nullable: false),
                    Sabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OrgID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    OrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<string>(type: "char(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkVisitPersonApply", x => x.WorkVisitPersonApplyID);
                });

            migrationBuilder.CreateTable(
                name: "WorkVisitPersonApplyHistory",
                columns: table => new
                {
                    WorkVisitPersonApplyHistoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkVisitPersonApplyID = table.Column<int>(type: "int", nullable: false),
                    WorkVisitApplyID = table.Column<int>(type: "int", nullable: false),
                    WorkVisitApplyStatus = table.Column<int>(type: "int", nullable: false),
                    Sabun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OrgID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OrgNameKor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    OrgNameEng = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    InsertUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkVisitPersonApplyHistory", x => x.WorkVisitPersonApplyHistoryID);
                });

            migrationBuilder.InsertData(
                table: "CommonCode",
                columns: new[] { "CommonCodeID", "CodeNameEng", "CodeNameKor", "GroupNo", "InsertName", "InsertOrgID", "InsertOrgNameEng", "InsertOrgNameKor", "InsertSabun", "IsActive", "IsSys", "Memo", "OrderNo", "SubCodeDesc", "SubCodeID", "UpdateDate" },
                values: new object[] { 1, "Location", "사업장", 1, null, null, null, null, "1", "Y", "Y", null, 0, null, null, null });

            migrationBuilder.InsertData(
                table: "CommonCode",
                columns: new[] { "CommonCodeID", "CodeNameEng", "CodeNameKor", "GroupNo", "InsertName", "InsertOrgID", "InsertOrgNameEng", "InsertOrgNameKor", "InsertSabun", "IsActive", "IsDelete", "IsSys", "Memo", "OrderNo", "SubCodeDesc", "SubCodeID", "UpdateDate" },
                values: new object[] { 2, "Seoul", "서울", 1, null, null, null, null, "1", "Y", "Y", "Y", null, 1, "1000", 0, null });

            migrationBuilder.InsertData(
                table: "CommonCode",
                columns: new[] { "CommonCodeID", "CodeNameEng", "CodeNameKor", "GroupNo", "InsertName", "InsertOrgID", "InsertOrgNameEng", "InsertOrgNameKor", "InsertSabun", "IsActive", "IsSys", "Memo", "OrderNo", "SubCodeDesc", "SubCodeID", "UpdateDate" },
                values: new object[,]
                {
                    { 3, "DB HiTek Bucheon Cam.", "DB HiTek 부천캠퍼스", 1, null, null, null, null, "1", "Y", "Y", null, 2, "2000", 1, null },
                    { 4, "DB HiTek Sangwoo Cam.", "DB HiTek 상우캠퍼스", 1, null, null, null, null, "1", "Y", "Y", null, 3, "3000", 2, null },
                    { 5, "DB GlobalChip", "DB GlobalChip", 1, null, null, null, null, "1", "Y", "Y", null, 4, "6000", 3, null },
                    { 6, "Company Type", "업체구분", 6, null, null, null, null, "1", "Y", "Y", null, 0, null, null, null },
                    { 7, "Head Office", "본사", 6, null, null, null, null, "1", "Y", "Y", null, 1, null, 0, null },
                    { 8, "Resident Partner", "상주협력사", 6, null, null, null, null, "1", "Y", "Y", null, 2, null, 1, null },
                    { 9, "NonResident Partner", "비상주협력사", 6, null, null, null, null, "1", "Y", "Y", null, 3, null, 2, null },
                    { 10, "WorkType Code", "공종코드", 10, null, null, null, null, "1", "Y", "N", null, 0, null, null, null },
                    { 11, "Electricity", "전기", 10, null, null, null, null, "1", "Y", "N", null, 1, null, 0, null },
                    { 12, "Construction", "토건", 10, null, null, null, null, "1", "Y", "N", null, 2, null, 1, null },
                    { 13, "Machine", "기계", 10, null, null, null, null, "1", "Y", "N", null, 3, null, 2, null },
                    { 14, "Maintenance", "정비", 10, null, null, null, null, "1", "Y", "N", null, 4, null, 3, null },
                    { 15, "Etc", "기타", 10, null, null, null, null, "1", "Y", "N", null, 5, null, 4, null },
                    { 16, "ImmStatus Code", "체류자격코드", 16, null, null, null, null, "1", "Y", "N", null, 0, null, null, null },
                    { 17, "F-2", "F-2(거주)", 16, null, null, null, null, "1", "Y", "N", null, 1, null, 0, null },
                    { 18, "F-4", "F-4(재외동포)", 16, null, null, null, null, "1", "Y", "N", null, 2, null, 1, null },
                    { 19, "F-5", "F-5(영주)", 16, null, null, null, null, "1", "Y", "N", null, 3, null, 2, null },
                    { 20, "F-6", "F-6(결혼이민)", 16, null, null, null, null, "1", "Y", "N", null, 4, null, 3, null },
                    { 21, "H-2", "H-2(방문취업)", 16, null, null, null, null, "1", "Y", "N", null, 5, null, 4, null },
                    { 22, "E-9", "E-9(비전문취업)", 16, null, null, null, null, "1", "Y", "N", null, 6, null, 5, null },
                    { 23, "E-9", "E-9(비전문취업)", 16, null, null, null, null, "1", "N", "N", null, 7, null, 6, null },
                    { 24, "C-4", "C-4(단기취업)", 16, null, null, null, null, "1", "Y", "N", null, 8, null, 7, null },
                    { 25, "D-7", "D-7(주재)", 16, null, null, null, null, "1", "Y", "N", null, 9, null, 8, null },
                    { 26, "Car Type", "차량구분코드", 26, null, null, null, null, "1", "Y", "N", null, 0, null, null, null },
                    { 27, "Passenger Car", "개인용", 26, null, null, null, null, "1", "Y", "N", null, 1, null, 0, null },
                    { 28, "Business Car", "업무용", 26, null, null, null, null, "1", "Y", "N", null, 2, null, 1, null }
                });

            migrationBuilder.InsertData(
                table: "CommonCode",
                columns: new[] { "CommonCodeID", "CodeNameEng", "CodeNameKor", "GroupNo", "InsertName", "InsertOrgID", "InsertOrgNameEng", "InsertOrgNameKor", "InsertSabun", "IsActive", "IsDelete", "IsSys", "Memo", "OrderNo", "SubCodeDesc", "SubCodeID", "UpdateDate" },
                values: new object[,]
                {
                    { 29, "", "화물(1톤이하)", 26, null, null, null, null, "1", "N", "Y", "N", null, 3, null, 2, null },
                    { 30, "", "화물(1톤초과)", 26, null, null, null, null, "1", "N", "Y", "N", null, 4, null, 3, null },
                    { 31, "", "중장비(특수용도)", 26, null, null, null, null, "1", "N", "Y", "N", null, 5, null, 4, null },
                    { 32, "", "기타", 26, null, null, null, null, "1", "N", "Y", "N", null, 6, null, 5, null }
                });

            migrationBuilder.InsertData(
                table: "CommonCode",
                columns: new[] { "CommonCodeID", "CodeNameEng", "CodeNameKor", "GroupNo", "InsertName", "InsertOrgID", "InsertOrgNameEng", "InsertOrgNameKor", "InsertSabun", "IsActive", "IsSys", "Memo", "OrderNo", "SubCodeDesc", "SubCodeID", "UpdateDate" },
                values: new object[,]
                {
                    { 33, "Visit Purpose", "방문목적코드", 33, null, null, null, null, "1", "Y", "N", null, 0, null, null, null },
                    { 34, "Meeting", "업무협의", 33, null, null, null, null, "1", "Y", "N", null, 1, null, 0, null },
                    { 35, "Construction,/Repair/Setup", "공사/수리/Setup", 33, null, null, null, null, "1", "Y", "N", null, 2, null, 1, null },
                    { 36, "Delivery", "납품", 33, null, null, null, null, "1", "Y", "N", null, 3, null, 2, null },
                    { 37, "Education", "교육", 33, null, null, null, null, "1", "Y", "N", null, 4, null, 3, null },
                    { 38, "Public Affairs Others", "공무 및 기타", 33, null, null, null, null, "1", "N", "N", null, 5, null, 4, null },
                    { 39, "Import Purpose", "반입목적코드", 39, null, null, null, null, "1", "Y", "N", null, 0, null, null, null },
                    { 40, "Setup", "Setup", 39, null, null, null, null, "1", "Y", "N", null, 1, null, 0, null },
                    { 41, "Construction/Repair", "공사/수리", 39, null, null, null, null, "1", "Y", "N", null, 2, null, 1, null },
                    { 42, "Equipment Change", "장비교체", 39, null, null, null, null, "1", "Y", "N", null, 3, null, 2, null }
                });

            migrationBuilder.InsertData(
                table: "CommonCode",
                columns: new[] { "CommonCodeID", "CodeNameEng", "CodeNameKor", "GroupNo", "InsertName", "InsertOrgID", "InsertOrgNameEng", "InsertOrgNameKor", "InsertSabun", "IsActive", "IsDelete", "IsSys", "Memo", "OrderNo", "SubCodeDesc", "SubCodeID", "UpdateDate" },
                values: new object[] { 43, "", "납품", 39, null, null, null, null, "1", "N", "Y", "N", null, 4, null, 3, null });

            migrationBuilder.InsertData(
                table: "CommonCode",
                columns: new[] { "CommonCodeID", "CodeNameEng", "CodeNameKor", "GroupNo", "InsertName", "InsertOrgID", "InsertOrgNameEng", "InsertOrgNameKor", "InsertSabun", "IsActive", "IsSys", "Memo", "OrderNo", "SubCodeDesc", "SubCodeID", "UpdateDate" },
                values: new object[,]
                {
                    { 44, "PreOrder", "선입고", 39, null, null, null, null, "1", "Y", "N", null, 5, null, 4, null },
                    { 45, "Export Import Purpose Type", "반출입목적", 45, null, null, null, null, "1", "Y", "Y", null, 0, null, null, null },
                    { 46, "Repair", "수리", 45, null, null, null, null, "1", "Y", "Y", null, 1, "2001", 0, null },
                    { 47, "Reclaim", "리클레임", 45, null, null, null, null, "1", "Y", "Y", null, 2, "2002", 1, null },
                    { 48, "Sale", "판매", 45, null, null, null, null, "1", "Y", "Y", null, 3, "2003", 2, null },
                    { 49, "Cleaning", "세정(재생)", 45, null, null, null, null, "1", "Y", "Y", null, 4, "2004", 3, null },
                    { 50, "Exchange", "교환", 45, null, null, null, null, "1", "Y", "Y", null, 5, "2005", 4, null },
                    { 51, "Transfer", "이체(이동)", 45, null, null, null, null, "1", "Y", "Y", null, 6, "2006", 5, null }
                });

            migrationBuilder.InsertData(
                table: "CommonCode",
                columns: new[] { "CommonCodeID", "CodeNameEng", "CodeNameKor", "GroupNo", "InsertName", "InsertOrgID", "InsertOrgNameEng", "InsertOrgNameKor", "InsertSabun", "IsActive", "IsDelete", "IsSys", "Memo", "OrderNo", "SubCodeDesc", "SubCodeID", "UpdateDate" },
                values: new object[] { 52, "", "기타", 45, null, null, null, null, "1", "N", "Y", "Y", null, 7, "2007", 6, null });

            migrationBuilder.InsertData(
                table: "CommonCode",
                columns: new[] { "CommonCodeID", "CodeNameEng", "CodeNameKor", "GroupNo", "InsertName", "InsertOrgID", "InsertOrgNameEng", "InsertOrgNameKor", "InsertSabun", "IsActive", "IsSys", "Memo", "OrderNo", "SubCodeDesc", "SubCodeID", "UpdateDate" },
                values: new object[,]
                {
                    { 53, "Analysis", "분석", 45, null, null, null, null, "1", "Y", "Y", null, 8, "2008", 7, null },
                    { 54, "Demo", "Demo", 45, null, null, null, null, "1", "Y", "Y", null, 9, "2009", 8, null }
                });

            migrationBuilder.InsertData(
                table: "CommonCode",
                columns: new[] { "CommonCodeID", "CodeNameEng", "CodeNameKor", "GroupNo", "InsertName", "InsertOrgID", "InsertOrgNameEng", "InsertOrgNameKor", "InsertSabun", "IsActive", "IsDelete", "IsSys", "Memo", "OrderNo", "SubCodeDesc", "SubCodeID", "UpdateDate" },
                values: new object[] { 55, "", "우수협력업체", 45, null, null, null, null, "1", "N", "Y", "Y", null, 10, "2010", 9, null });

            migrationBuilder.InsertData(
                table: "CommonCode",
                columns: new[] { "CommonCodeID", "CodeNameEng", "CodeNameKor", "GroupNo", "InsertName", "InsertOrgID", "InsertOrgNameEng", "InsertOrgNameKor", "InsertSabun", "IsActive", "IsSys", "Memo", "OrderNo", "SubCodeDesc", "SubCodeID", "UpdateDate" },
                values: new object[,]
                {
                    { 56, "Warranty", "Warranty", 45, null, null, null, null, "1", "Y", "Y", null, 11, "2011", 10, null },
                    { 57, "Return", "반환", 45, null, null, null, null, "1", "Y", "Y", null, 12, "2012", 11, null },
                    { 58, "Tablets", "태블릿", 45, null, null, null, null, "1", "Y", "Y", null, 13, "", 12, null },
                    { 60, "Export Import Type", "반출입구분", 60, null, null, null, null, "1", "Y", "Y", null, 0, null, null, null },
                    { 61, "Property", "자산", 60, null, null, null, null, "1", "Y", "Y", null, 1, null, 0, null },
                    { 62, "Repair", "수리", 60, null, null, null, null, "1", "Y", "Y", null, 2, null, 1, null },
                    { 63, "Notebook", "노트북", 60, null, null, null, null, "1", "Y", "Y", null, 3, null, 2, null },
                    { 64, "Export Type", "반출구분", 64, null, null, null, null, "1", "Y", "Y", null, 0, null, null, null },
                    { 65, "External Company", "외부업체", 64, null, null, null, null, "1", "Y", "Y", null, 1, "O", 0, null },
                    { 66, "Inter Factory Movement", "공장간이동", 64, null, null, null, null, "1", "Y", "Y", null, 2, "I", 1, null },
                    { 67, "External Company Passage", "외부업체경유공장간이동", 64, null, null, null, null, "1", "Y", "Y", null, 3, "OI", 2, null },
                    { 68, "Personal Notebook", "개인노트북", 64, null, null, null, null, "1", "Y", "Y", null, 4, "N", 3, null },
                    { 69, "Rental Notebook", "대여노트북", 64, null, null, null, null, "1", "Y", "Y", null, 5, "", 4, null },
                    { 70, "Import Type", "반입구분", 70, null, null, null, null, "1", "Y", "Y", null, 0, null, null, null },
                    { 71, "Bring", "반입요", 70, null, null, null, null, "1", "Y", "Y", null, 1, "12001", 0, null },
                    { 72, "Do Not Bring", "반입불요", 70, null, null, null, null, "1", "Y", "Y", null, 2, "12002", 1, null }
                });

            migrationBuilder.InsertData(
                table: "CommonCode",
                columns: new[] { "CommonCodeID", "CodeNameEng", "CodeNameKor", "GroupNo", "InsertName", "InsertOrgID", "InsertOrgNameEng", "InsertOrgNameKor", "InsertSabun", "IsActive", "IsDelete", "IsSys", "Memo", "OrderNo", "SubCodeDesc", "SubCodeID", "UpdateDate" },
                values: new object[,]
                {
                    { 73, null, "매각", 70, null, null, null, null, "1", "N", "Y", "Y", null, 3, "12003", 2, null },
                    { 74, "", "반입불요(무상)", 70, null, null, null, null, "1", "N", "Y", "Y", null, 4, "12004", 3, null }
                });

            migrationBuilder.InsertData(
                table: "CommonCode",
                columns: new[] { "CommonCodeID", "CodeNameEng", "CodeNameKor", "GroupNo", "InsertName", "InsertOrgID", "InsertOrgNameEng", "InsertOrgNameKor", "InsertSabun", "IsActive", "IsSys", "Memo", "OrderNo", "SubCodeDesc", "SubCodeID", "UpdateDate" },
                values: new object[,]
                {
                    { 75, "Replacement Item Return", "대체품반입요", 70, null, null, null, null, "1", "Y", "Y", null, 5, "12006", 4, null },
                    { 76, "Replacement Item Prereceiving", "대체품선입고", 70, null, null, null, null, "1", "Y", "Y", null, 6, "12007", 5, null },
                    { 77, "Import Way Type", "반입자구분", 77, null, null, null, null, "1", "Y", "Y", null, 0, null, null, null },
                    { 78, "Agency Registration", "대행등록", 77, null, null, null, null, "1", "Y", "Y", null, 1, null, 0, null },
                    { 79, "Self", "본인", 77, null, null, null, null, "1", "Y", "Y", null, 2, null, 1, null },
                    { 80, "Delivery Method Type", "반출물전달방법", 80, null, null, null, null, "1", "Y", "Y", null, 0, null, null, null },
                    { 81, "Receipt Visit", "방문수령", 80, null, null, null, null, "1", "Y", "Y", null, 1, "8001", 0, null },
                    { 82, "Mail/Delivery", "우편/택배", 80, null, null, null, null, "1", "Y", "Y", null, 2, "8002", 1, null },
                    { 83, "Receipt Agent", "대리인수령", 80, null, null, null, null, "1", "Y", "Y", null, 3, "8003", 2, null },
                    { 84, "Hand Carry", "핸드캐리", 80, null, null, null, null, "1", "Y", "Y", null, 4, "8004", 3, null },
                    { 85, "Logistics Vehicle", "물류차량", 80, null, null, null, null, "1", "Y", "Y", null, 5, "8005", 4, null },
                    { 86, "Place", "장소코드", 86, null, null, null, null, "1", "Y", "Y", null, 0, null, null, null },
                    { 87, "Bucheon Cam. Fab.", "부천캠퍼스 본관", 86, null, null, null, null, "1", "Y", "Y", null, 1, null, 0, null },
                    { 88, "Bucheon Campus Shocoli", "부천캠퍼스 쇼콜리동", 86, null, null, null, null, "1", "Y", "Y", null, 2, null, 1, null },
                    { 89, "Bucheon Cam. Kindergarden", "부천캠퍼스 어린이집", 86, null, null, null, null, "1", "Y", "Y", null, 3, null, 2, null },
                    { 90, "Sangwoo Cam. Fab.", "상우캠퍼스 본관", 86, null, null, null, null, "1", "Y", "Y", null, 4, null, 3, null },
                    { 91, "Sangwoo Admin", "상우캠퍼스 어드민동", 86, null, null, null, null, "1", "Y", "Y", null, 5, null, 4, null },
                    { 92, "DBGlobalChip", "DBGlobalChip", 86, null, null, null, null, "1", "Y", "Y", null, 6, null, 5, null },
                    { 93, "Clean Room", "클린룸", 86, null, null, null, null, "1", "Y", "Y", null, 7, null, 6, null },
                    { 94, "↓↓Accident Preparedness Material Handling Facility↓↓", "↓↓사고대비물질 취급시설↓↓", 86, null, null, null, null, "1", "Y", "Y", null, 8, null, 7, null },
                    { 95, "Chemical Storage Warehouse", "공통-제한구역-황색-가스케미칼보관창고", 86, null, null, null, null, "1", "Y", "Y", null, 9, null, 8, null },
                    { 96, "Chemical Analysis Room", "공통-제한구역-황색-화학분석실", 86, null, null, null, null, "1", "Y", "Y", null, 10, null, 9, null },
                    { 97, "Defect Analysis Office", "공통-제한구역-황색-불량분석실", 86, null, null, null, null, "1", "Y", "Y", null, 11, null, 10, null },
                    { 98, "CCSS", "공통-제한구역-황색-CCSS", 86, null, null, null, null, "1", "Y", "Y", null, 12, null, 11, null },
                    { 99, "MBR", "부천-제한구역-황색-MBR", 86, null, null, null, null, "1", "Y", "Y", null, 13, null, 12, null },
                    { 100, "WWT", "상우-제한구역-황색-WWT", 86, null, null, null, null, "1", "Y", "Y", null, 14, null, 13, null },
                    { 101, "Sulfuric Acid Tank", "부천-제한구역-황색-황산탱크(옥상)", 86, null, null, null, null, "1", "Y", "Y", null, 15, null, 14, null },
                    { 102, "Carry Item", "휴대물품구분코드", 102, null, null, null, null, "1", "Y", "N", null, 0, null, null, null },
                    { 103, "Laptops and PC", "노트북 및 PC ", 102, null, null, null, null, "1", "Y", "N", null, 1, null, 0, null },
                    { 104, "Work Tools", "작업공도구", 102, null, null, null, null, "1", "Y", "N", null, 2, null, 1, null }
                });

            migrationBuilder.InsertData(
                table: "CommonCode",
                columns: new[] { "CommonCodeID", "CodeNameEng", "CodeNameKor", "GroupNo", "InsertName", "InsertOrgID", "InsertOrgNameEng", "InsertOrgNameKor", "InsertSabun", "IsActive", "IsDelete", "IsSys", "Memo", "OrderNo", "SubCodeDesc", "SubCodeID", "UpdateDate" },
                values: new object[] { 105, "Camera", "카메라", 102, null, null, null, null, "1", "Y", "Y", "N", null, 3, null, 2, null });

            migrationBuilder.InsertData(
                table: "CommonCode",
                columns: new[] { "CommonCodeID", "CodeNameEng", "CodeNameKor", "GroupNo", "InsertName", "InsertOrgID", "InsertOrgNameEng", "InsertOrgNameKor", "InsertSabun", "IsActive", "IsSys", "Memo", "OrderNo", "SubCodeDesc", "SubCodeID", "UpdateDate" },
                values: new object[,]
                {
                    { 106, "Etc", "기타", 102, null, null, null, null, "1", "Y", "N", null, 4, null, 3, null },
                    { 107, "Card Issue Type", "출입증발급구분", 107, null, null, null, null, "1", "Y", "Y", null, 0, null, null, null },
                    { 108, "New", "신규", 107, null, null, null, null, "1", "Y", "Y", null, 1, null, 0, null },
                    { 109, "Reissuance", "재발급", 107, null, null, null, null, "1", "Y", "Y", null, 2, null, 1, null },
                    { 110, "Visit Apply Type", "방문신청구분", 110, null, null, null, null, "1", "Y", "Y", null, 0, null, null, null },
                    { 111, "Application Visit", "방문신청", 110, null, null, null, null, "1", "Y", "Y", null, 1, null, 0, null },
                    { 112, "Application Work Visit", "공사출입인원신청", 110, null, null, null, null, "1", "Y", "Y", null, 2, null, 1, null },
                    { 113, "Safety Education", "안전교육", 110, null, null, null, null, "1", "Y", "Y", null, 3, null, 2, null },
                    { 114, "Visitor Manual ", "방문객수기등록", 110, null, null, null, null, "1", "Y", "Y", null, 4, null, 3, null }
                });

            migrationBuilder.InsertData(
                table: "CommonCode",
                columns: new[] { "CommonCodeID", "CodeNameEng", "CodeNameKor", "GroupNo", "InsertName", "InsertOrgID", "InsertOrgNameEng", "InsertOrgNameKor", "InsertSabun", "IsActive", "IsDelete", "IsSys", "Memo", "OrderNo", "SubCodeDesc", "SubCodeID", "UpdateDate" },
                values: new object[] { 115, "Delivery", "택배", 110, null, null, null, null, "1", "N", "Y", "Y", null, 5, null, 4, null });

            migrationBuilder.InsertData(
                table: "CommonCode",
                columns: new[] { "CommonCodeID", "CodeNameEng", "CodeNameKor", "GroupNo", "InsertName", "InsertOrgID", "InsertOrgNameEng", "InsertOrgNameKor", "InsertSabun", "IsActive", "IsSys", "Memo", "OrderNo", "SubCodeDesc", "SubCodeID", "UpdateDate" },
                values: new object[,]
                {
                    { 116, "Vip Type", "VIP구분코드", 116, null, null, null, null, "1", "Y", "N", null, 0, null, null, null },
                    { 117, "Important Buyer", "중요바이어", 116, null, null, null, null, "1", "Y", "N", null, 1, null, 0, null },
                    { 118, "SMS Type", "SMS발송구분", 118, null, null, null, null, "1", "Y", "Y", null, 0, null, null, null },
                    { 119, "", "방문객 개인정보동의 및 교육 안내", 118, null, null, null, null, "1", "Y", "Y", null, 1, null, 0, null },
                    { 120, "Company Status", "업체상태", 120, null, null, null, null, "1", "Y", "Y", null, 0, null, null, null },
                    { 121, "Waiting Approval", "승인대기", 120, null, null, null, null, "1", "Y", "Y", null, 1, null, 0, null },
                    { 122, "Approved", "승인완료", 120, null, null, null, null, "1", "Y", "Y", null, 2, null, 1, null },
                    { 123, "Companion", "반려", 120, null, null, null, null, "1", "Y", "Y", null, 3, null, 2, null },
                    { 124, "Card Issue Status", "출입증발급상태", 124, null, null, null, null, "1", "Y", "Y", null, 0, null, null, null },
                    { 125, "Issuance", "발급", 124, null, null, null, null, "1", "Y", "Y", null, 1, null, 0, null },
                    { 126, "Not issued", "미발급", 124, null, null, null, null, "1", "Y", "Y", null, 2, null, 1, null },
                    { 127, "CardApplyStatus", "출입증신청상태", 127, null, null, null, null, "1", "Y", "Y", null, 0, null, null, null },
                    { 128, "Waiting Approval", "승인대기", 127, null, null, null, null, "1", "Y", "Y", null, 1, null, 0, null },
                    { 129, "Approved", "승인완료", 127, null, null, null, null, "1", "Y", "Y", null, 2, null, 1, null },
                    { 130, "Companion", "반려", 127, null, null, null, null, "1", "Y", "Y", null, 3, null, 2, null },
                    { 131, "Temp Card Issue Status", "임시증발급상태", 131, null, null, null, null, "1", "Y", "Y", null, 0, null, null, null },
                    { 132, "Issuance", "발급", 131, null, null, null, null, "1", "Y", "Y", null, 1, null, 0, null },
                    { 133, "Collection", "회수", 131, null, null, null, null, "1", "Y", "Y", null, 2, null, 1, null }
                });

            migrationBuilder.InsertData(
                table: "CommonCode",
                columns: new[] { "CommonCodeID", "CodeNameEng", "CodeNameKor", "GroupNo", "InsertName", "InsertOrgID", "InsertOrgNameEng", "InsertOrgNameKor", "InsertSabun", "IsActive", "IsDelete", "IsSys", "Memo", "OrderNo", "SubCodeDesc", "SubCodeID", "UpdateDate" },
                values: new object[,]
                {
                    { 134, "Companion", "반려", 131, null, null, null, null, "1", "Y", "Y", "Y", null, 3, null, 2, null },
                    { 135, "Waiting Approval", "승인대기", 131, null, null, null, null, "1", "Y", "Y", "Y", null, 4, null, 3, null }
                });

            migrationBuilder.InsertData(
                table: "CommonCode",
                columns: new[] { "CommonCodeID", "CodeNameEng", "CodeNameKor", "GroupNo", "InsertName", "InsertOrgID", "InsertOrgNameEng", "InsertOrgNameKor", "InsertSabun", "IsActive", "IsSys", "Memo", "OrderNo", "SubCodeDesc", "SubCodeID", "UpdateDate" },
                values: new object[,]
                {
                    { 136, "Safety Violation Status", "안전수칙위반상태", 136, null, null, null, null, "1", "Y", "Y", null, 0, null, null, null },
                    { 137, "New Issue", "신규발행", 136, null, null, null, null, "1", "Y", "Y", null, 1, null, 0, null },
                    { 138, "Prevention Measures Registration", "방지대책등록", 136, null, null, null, null, "1", "Y", "Y", null, 2, null, 1, null },
                    { 139, "Prevention Measures Approved", "방지대책승인", 136, null, null, null, null, "1", "Y", "Y", null, 3, null, 2, null },
                    { 140, "Request Reregistration Preventive Measures", "방지대책재등록요청", 136, null, null, null, null, "1", "Y", "Y", null, 4, null, 3, null },
                    { 141, "BlackList Status", "블랙리스트상태", 141, null, null, null, null, "1", "Y", "Y", null, 0, null, null, null },
                    { 142, "Registration Request", "등록요청", 141, null, null, null, null, "1", "Y", "Y", null, 1, null, 0, null },
                    { 143, "Registration", "등록", 141, null, null, null, null, "1", "Y", "Y", null, 2, null, 1, null },
                    { 144, "Visit Apply Status", "방문신청상태", 144, null, null, null, null, "1", "Y", "Y", null, 0, null, null, null },
                    { 145, "Waiting Approval", "승인대기", 144, null, null, null, null, "1", "Y", "Y", null, 1, null, 0, null },
                    { 146, "Approved", "승인완료", 144, null, null, null, null, "1", "Y", "Y", null, 2, null, 1, null },
                    { 147, "Companion", "반려", 144, null, null, null, null, "1", "Y", "Y", null, 3, null, 2, null }
                });

            migrationBuilder.InsertData(
                table: "CommonCode",
                columns: new[] { "CommonCodeID", "CodeNameEng", "CodeNameKor", "GroupNo", "InsertName", "InsertOrgID", "InsertOrgNameEng", "InsertOrgNameKor", "InsertSabun", "IsActive", "IsDelete", "IsSys", "Memo", "OrderNo", "SubCodeDesc", "SubCodeID", "UpdateDate" },
                values: new object[,]
                {
                    { 148, "Not Returned", "미반납", 144, null, null, null, null, "1", "N", "Y", "Y", null, 4, null, 3, null },
                    { 149, "End Visit", "방문종료", 144, null, null, null, null, "1", "N", "Y", "Y", null, 5, null, 4, null }
                });

            migrationBuilder.InsertData(
                table: "CommonCode",
                columns: new[] { "CommonCodeID", "CodeNameEng", "CodeNameKor", "GroupNo", "InsertName", "InsertOrgID", "InsertOrgNameEng", "InsertOrgNameKor", "InsertSabun", "IsActive", "IsSys", "Memo", "OrderNo", "SubCodeDesc", "SubCodeID", "UpdateDate" },
                values: new object[,]
                {
                    { 150, "Visit Status", "방문상태", 150, null, null, null, null, "1", "Y", "Y", null, 0, null, null, null },
                    { 151, "Waiting Visit", "방문대기", 150, null, null, null, null, "1", "Y", "Y", null, 1, null, 0, null },
                    { 152, "Visit", "방문", 150, null, null, null, null, "1", "Y", "Y", null, 2, null, 1, null },
                    { 153, "Visit Completed", "방문완료", 150, null, null, null, null, "1", "Y", "Y", null, 3, null, 2, null },
                    { 154, "Not Returned", "미반납", 150, null, null, null, null, "1", "Y", "Y", null, 4, null, 3, null },
                    { 155, "End Visit", "방문종료", 150, null, null, null, null, "1", "Y", "Y", null, 5, null, 4, null },
                    { 156, "Education Apply Status", "교육신청상태", 156, null, null, null, null, "1", "Y", "Y", null, 0, null, null, null },
                    { 157, "Waiting Approval", "승인대기", 156, null, null, null, null, "1", "Y", "Y", null, 1, null, 0, null },
                    { 158, "Approved", "승인완료", 156, null, null, null, null, "1", "Y", "Y", null, 2, null, 1, null },
                    { 159, "Education Complete Status", "교육이수상태", 159, null, null, null, null, "1", "Y", "Y", null, 0, null, null, null },
                    { 160, "Not Complete", "미이수", 159, null, null, null, null, "1", "Y", "Y", null, 1, null, 0, null },
                    { 161, "Complete", "이수", 159, null, null, null, null, "1", "Y", "Y", null, 2, null, 1, null },
                    { 162, "Export Import Status", "반출입상태", 162, null, null, null, null, "1", "Y", "Y", null, 0, null, null, null },
                    { 163, "Waiting Approval", "승인대기", 162, null, null, null, null, "1", "Y", "Y", null, 1, null, 0, null },
                    { 164, "Approved", "승인완료", 162, null, null, null, null, "1", "Y", "Y", null, 2, null, 1, null },
                    { 165, "Companion", "반려", 162, null, null, null, null, "1", "Y", "Y", null, 3, null, 2, null },
                    { 166, "Under Approval", "결재중", 162, null, null, null, null, "1", "Y", "Y", null, 4, null, 3, null },
                    { 167, "Payment Completed", "결재완료", 162, null, null, null, null, "1", "Y", "Y", null, 5, null, 4, null },
                    { 168, "Carry Item Status", "휴대물품반입반출상태", 168, null, null, null, null, "1", "Y", "Y", null, 0, null, null, null },
                    { 169, "Waiting Bring", "반입대기", 168, null, null, null, null, "1", "Y", "Y", null, 1, "13001", 0, null },
                    { 170, "Waiting Take Out", "반출대기", 168, null, null, null, null, "1", "Y", "Y", null, 2, "13002", 1, null },
                    { 171, "Waiting Confirmation", "확인대기", 168, null, null, null, null, "1", "Y", "Y", null, 3, "13003", 2, null },
                    { 172, "Transfer Completed", "반출완료", 168, null, null, null, null, "1", "Y", "Y", null, 4, "13004", 3, null },
                    { 173, "Not Bring", "미반입", 168, null, null, null, null, "1", "Y", "Y", null, 5, "", 4, null },
                    { 174, "Not Submitted", "미반출", 168, null, null, null, null, "1", "Y", "Y", null, 6, "", 5, null },
                    { 175, "Carry In Completed", "반입완료", 168, null, null, null, null, "1", "Y", "Y", null, 7, "", 6, null },
                    { 176, "Confirmation Completed", "확인완료", 168, null, null, null, null, "1", "Y", "Y", null, 8, "", 7, null },
                    { 177, "Carry Item Apply Status", "휴대물품신청상태", 177, null, null, null, null, "1", "Y", "Y", null, 0, null, null, null },
                    { 178, "Application", "신청", 177, null, null, null, null, "1", "Y", "Y", null, 1, null, 0, null },
                    { 179, "Approved", "승인", 177, null, null, null, null, "1", "Y", "Y", null, 2, null, 1, null },
                    { 180, "Companion", "반려", 177, null, null, null, null, "1", "Y", "Y", null, 3, null, 2, null },
                    { 181, "Under Approval", "결재중", 177, null, null, null, null, "1", "Y", "Y", null, 4, null, 3, null },
                    { 182, "Payment Completed", "결재완료", 177, null, null, null, null, "1", "Y", "Y", null, 5, null, 4, null },
                    { 183, "Meal Type", "식수구분", 183, null, null, null, null, "1", "Y", "Y", null, 0, null, null, null },
                    { 184, "Unusual Meal", "이상식수", 183, null, null, null, null, "1", "Y", "Y", null, 1, null, 0, null },
                    { 185, "Breakfast", "조식", 183, null, null, null, null, "1", "Y", "Y", null, 2, null, 1, null },
                    { 186, "Lunch", "중식", 183, null, null, null, null, "1", "Y", "Y", null, 3, null, 2, null },
                    { 187, "Dinner", "석식", 183, null, null, null, null, "1", "Y", "Y", null, 4, null, 3, null },
                    { 188, "LatenightMeal", "야식", 183, null, null, null, null, "1", "Y", "Y", null, 5, null, 4, null },
                    { 189, "Term Type", "계절구분", 189, null, null, null, null, "1", "Y", "Y", null, 0, null, null, null },
                    { 190, "Summer Season Term", "하계", 189, null, null, null, null, "1", "Y", "Y", null, 1, null, 0, null },
                    { 191, "Winter Season Term", "동계", 189, null, null, null, null, "1", "Y", "Y", null, 2, null, 1, null },
                    { 192, "Meal Menual Type", "수기등록구분", 192, null, null, null, null, "1", "Y", "Y", null, 0, null, null, null },
                    { 193, "Reader", "식수리더", 192, null, null, null, null, "1", "Y", "Y", null, 1, null, 0, null },
                    { 194, "Menual", "수기등록", 192, null, null, null, null, "1", "Y", "Y", null, 2, null, 1, null },
                    { 195, "Foreigner Type", "외국인구분", 195, null, null, null, null, "1", "Y", "N", null, 0, null, null, null },
                    { 196, "Domestic", "내국인", 195, null, null, null, null, "1", "Y", "N", null, 1, null, 0, null },
                    { 197, "Foreigner", "외국인", 195, null, null, null, null, "1", "Y", "N", null, 2, null, 1, null },
                    { 198, "Gender Type", "성별구분", 198, null, null, null, null, "1", "Y", "N", null, 0, null, null, null },
                    { 199, "Man", "남", 198, null, null, null, null, "1", "Y", "N", null, 1, null, 0, null },
                    { 200, "Woman", "여", 198, null, null, null, null, "1", "Y", "N", null, 2, null, 1, null },
                    { 201, "Visitor Type", "방문자구분", 201, null, null, null, null, "1", "Y", "Y", null, 0, null, null, null },
                    { 202, "Employees", "임직원", 201, null, null, null, null, "1", "Y", "Y", null, 1, null, 0, null },
                    { 203, "Visitor", "방문자", 201, null, null, null, null, "1", "Y", "Y", null, 2, null, 1, null },
                    { 204, "Person Status", "재직상태", 204, null, null, null, null, "1", "Y", "Y", null, 0, null, null, null },
                    { 205, "Hold Office", "재직", 204, null, null, null, null, "1", "Y", "Y", null, 1, null, 0, null },
                    { 206, "Take time Off", "휴직", 204, null, null, null, null, "1", "Y", "Y", null, 2, null, 1, null },
                    { 207, "Rretirement", "퇴직", 204, null, null, null, null, "1", "Y", "Y", null, 3, null, 2, null },
                    { 208, "Approval Type", "결재유형", 208, null, null, null, null, "1", "Y", "N", null, 0, null, null, null },
                    { 209, "Add Person", "사원추가", 208, null, null, null, null, "1", "Y", "N", null, 1, null, 0, null },
                    { 210, "Add Card", "카드추가", 208, null, null, null, null, "1", "Y", "N", null, 2, null, 1, null },
                    { 211, "Apply Export Import ", "반출입신청", 208, null, null, null, null, "1", "Y", "N", null, 3, null, 2, null },
                    { 212, "Apply Work", "공사신청", 208, null, null, null, null, "1", "Y", "N", null, 4, null, 3, null },
                    { 213, "Apply Safety Education", "안전교육신청", 208, null, null, null, null, "1", "Y", "N", null, 5, null, 4, null },
                    { 214, "Approval Sys Type", "결재자유형", 214, null, null, null, null, "1", "Y", "N", null, 0, null, null, null },
                    { 215, "Management System", "통문관리시스템", 214, null, null, null, null, "1", "Y", "N", null, 1, null, 0, null },
                    { 216, "ERP", "ERP", 214, null, null, null, null, "1", "Y", "N", null, 2, null, 1, null },
                    { 217, "User Settings", "사용자설정", 214, null, null, null, null, "1", "Y", "N", null, 3, null, 2, null },
                    { 218, "Person Type", "회원구분", 218, null, null, null, null, "1", "Y", "Y", null, 0, null, null, null },
                    { 219, "Executives", "임직원", 218, null, null, null, null, "1", "Y", "Y", null, 1, null, 0, null },
                    { 220, "Resident Employee", "상주직원", 218, null, null, null, null, "1", "Y", "Y", null, 2, null, 1, null },
                    { 221, "NonResident Manager", "비상주관리자", 218, null, null, null, null, "1", "Y", "Y", null, 3, null, 2, null },
                    { 222, "NonResident Employee", "비상주직원", 218, null, null, null, null, "1", "Y", "Y", null, 4, null, 3, null },
                    { 223, "Visitor", "방문객", 218, null, null, null, null, "1", "Y", "Y", null, 5, null, 4, null },
                    { 224, "Etc", "기타", 39, null, null, null, null, "1", "Y", "N", null, 6, null, 5, null },
                    { 225, "WorkApply Status", "공사신청상태", 225, null, null, null, null, "1", "Y", "Y", null, 0, null, null, null },
                    { 226, "Waiting Approval", "승인대기", 225, null, null, null, null, "1", "Y", "Y", null, 1, null, 0, null },
                    { 227, "Approved", "승인완료", 225, null, null, null, null, "1", "Y", "Y", null, 2, null, 1, null },
                    { 228, "Companion", "반려", 225, null, null, null, null, "1", "Y", "Y", null, 3, null, 2, null },
                    { 229, "WorkVisitApply Status", "공사출입신청상태", 229, null, null, null, null, "1", "Y", "Y", null, 0, null, null, null },
                    { 230, "Waiting Approval", "승인대기", 229, null, null, null, null, "1", "Y", "Y", null, 1, null, 0, null },
                    { 231, "Approved", "승인완료", 229, null, null, null, null, "1", "Y", "Y", null, 2, null, 1, null },
                    { 232, "Companion", "반려", 229, null, null, null, null, "1", "Y", "Y", null, 3, null, 2, null },
                    { 233, "Visit Manual Purpose", "방문수기등록방문목적", 233, null, null, null, null, "1", "Y", "N", null, 0, null, null, null },
                    { 234, "Delivery", "택배", 233, null, null, null, null, "1", "Y", "N", null, 1, null, 0, null },
                    { 235, "BlackList Type", "등록사유구분", 235, null, null, null, null, "1", "Y", "N", null, 0, null, null, null },
                    { 236, "Retired Employee", "퇴직임직원", 235, null, null, null, null, "1", "Y", "N", null, 1, null, 0, null },
                    { 237, "Violation Safety Rules", "안전수칙위반", 235, null, null, null, null, "1", "Y", "N", null, 2, null, 1, null },
                    { 238, "Violation Security Rules", "보안수칙위반", 235, null, null, null, null, "1", "Y", "N", null, 3, null, 2, null },
                    { 239, "Etc", "기타", 235, null, null, null, null, "1", "Y", "N", null, 4, null, 3, null },
                    { 240, "Release", "해제", 141, null, null, null, null, "1", "Y", "N", null, 3, null, 2, null },
                    { 241, "Carry Item Import Type", "휴대물품반입구분", 241, null, null, null, null, "1", "Y", "N", null, 0, null, null, null },
                    { 242, "Waiting Bring", "반입대기", 241, null, null, null, null, "1", "Y", "N", null, 1, null, 0, null },
                    { 243, "Carryin Processing", "반입처리", 241, null, null, null, null, "1", "Y", "N", null, 2, null, 1, null },
                    { 244, "Take Out", "반출요", 241, null, null, null, null, "1", "Y", "N", null, 3, null, 2, null },
                    { 245, "Not Allowed Take Out", "반출불가", 241, null, null, null, null, "1", "Y", "N", null, 4, null, 3, null },
                    { 246, "No Need Take Out", "반출불필요(유상)", 241, null, null, null, null, "1", "Y", "N", null, 5, null, 4, null },
                    { 247, "No Need Take Out(free)", "반출불필요(무상)", 241, null, null, null, null, "1", "Y", "N", null, 6, null, 5, null },
                    { 248, "Receipt Confirmation", "입고확인 ", 241, null, null, null, null, "1", "Y", "N", null, 7, null, 6, null },
                    { 249, "Card Type", "출입증구분", 249, null, null, null, null, "1", "Y", "Y", null, 0, null, null, null },
                    { 250, "General Pass", "일반", 249, null, null, null, null, "1", "Y", "N", null, 1, null, 0, null },
                    { 251, "Visitor Pass", "방문증", 249, null, null, null, null, "1", "Y", "N", null, 2, null, 1, null },
                    { 252, "Lost", "분실", 124, null, null, null, null, "1", "Y", "N", null, 3, null, 2, null }
                });

            migrationBuilder.InsertData(
                table: "CommonCode",
                columns: new[] { "CommonCodeID", "CodeNameEng", "CodeNameKor", "GroupNo", "InsertName", "InsertOrgID", "InsertOrgNameEng", "InsertOrgNameKor", "InsertSabun", "IsActive", "IsDelete", "IsSys", "Memo", "OrderNo", "SubCodeDesc", "SubCodeID", "UpdateDate" },
                values: new object[] { 253, "Dormancy", "휴면", 124, null, null, null, null, "1", "Y", "Y", "N", null, 4, null, 3, null });

            migrationBuilder.InsertData(
                table: "CommonCode",
                columns: new[] { "CommonCodeID", "CodeNameEng", "CodeNameKor", "GroupNo", "InsertName", "InsertOrgID", "InsertOrgNameEng", "InsertOrgNameKor", "InsertSabun", "IsActive", "IsSys", "Memo", "OrderNo", "SubCodeDesc", "SubCodeID", "UpdateDate" },
                values: new object[,]
                {
                    { 254, "Unit", "단위", 254, null, null, null, null, "1", "Y", "N", null, 0, null, null, null },
                    { 255, "Count", "개", 254, null, null, null, null, "1", "Y", "N", null, 1, null, 0, null },
                    { 256, "Mark", "대", 254, null, null, null, null, "1", "Y", "N", null, 2, null, 1, null },
                    { 257, "EA", "EA", 254, null, null, null, null, "1", "Y", "N", null, 3, null, 2, null },
                    { 258, "SIK", "SIK", 254, null, null, null, null, "1", "Y", "N", null, 4, null, 3, null },
                    { 259, "Import Confirmation", "반입확인", 162, null, null, null, null, "1", "Y", "Y", null, 6, null, 5, null },
                    { 260, "Export Confirmation", "반출확인", 162, null, null, null, null, "1", "Y", "Y", null, 7, null, 6, null },
                    { 261, "Organic Recovery Room", "상우-제한구역-황색-유기회수실", 86, null, null, null, null, "1", "Y", "Y", null, 16, null, 15, null }
                });

            migrationBuilder.InsertData(
                table: "Holiday",
                columns: new[] { "HolidayID", "HolidayDate", "HolidayName", "InsertName", "InsertOrgID", "InsertOrgNameEng", "InsertOrgNameKor", "InsertSabun", "UpdateDate" },
                values: new object[,]
                {
                    { 1, "2023-01-01", "설날", null, null, null, null, "1", null },
                    { 2, "2023-03-01", "삼일절", null, null, null, null, "1", null },
                    { 3, "2023-05-05", "어린이날", null, null, null, null, "1", null }
                });

            migrationBuilder.InsertData(
                table: "Level",
                columns: new[] { "LevelID", "InsertName", "InsertOrgID", "InsertOrgNameEng", "InsertOrgNameKor", "InsertSabun", "LevelName", "UpdateDate" },
                values: new object[,]
                {
                    { 1, null, null, null, null, "1", "마스터관리자", null },
                    { 2, null, null, null, null, "1", "일반관리자", null },
                    { 3, null, null, null, null, "1", "임직원(일반)", null },
                    { 4, null, null, null, null, "1", "임직원(인사)", null },
                    { 5, null, null, null, null, "1", "임직원(ESH)", null },
                    { 6, null, null, null, null, "1", "임직원(영양사)", null },
                    { 7, null, null, null, null, "1", "비상주", null },
                    { 8, null, null, null, null, "1", "보안실", null },
                    { 9, null, null, null, null, "1", "비상주업체", null }
                });

            migrationBuilder.InsertData(
                table: "Material",
                columns: new[] { "MaterialID", "DIV_CODE", "STOCK_CODE", "STOCK_NM", "STOCK_RULE", "STOCK_UNIT", "W_DATE" },
                values: new object[,]
                {
                    { 1, "11", "B_GRINDING_CHG", "Back Grinding Service Charge", "", "AU", "2005-06-07" },
                    { 2, "11", "N00MSKIG", "MASK", "", "AU", "2019-02-21" },
                    { 3, "11", "N00MSKME", "MASK", "", "AU", "2017-12-01" },
                    { 4, "11", "N00PRBSJ", "PROBE CARD", "", "AU", "2017-12-06" },
                    { 5, "11", "N00SPCIG", "Sample Wafer", "", "AU", "2020-02-25" },
                    { 6, "11", "N00SPCME", "Sample Wafer", "", "AU", "2018-03-14" },
                    { 7, "11", "N00SPCMO", "Sample Wafer", "", "AU", "2019-04-22" },
                    { 8, "11", "N00SPCSJ", "Sample Wafer", "", "AU", "2018-12-11" },
                    { 9, "11", "N00TSCIG", "SCRAP", "", "AU", "2019-03-08" },
                    { 10, "11", "N00TSCMO", "SCRAP", "", "AU", "2020-02-28" },
                    { 11, "11", "N00TTSMO", "TEST", "", "AU", "2017-09-27" },
                    { 12, "11", "N06MSKMO", "MASK", "", "AU", "2016-06-09" },
                    { 13, "11", "N06TTSMO", "TEST", "", "AU", "2016-06-09" },
                    { 14, "11", "N09APASF", "PRICE ADJUSTMENT", "", "AU", "2013-03-04" },
                    { 15, "11", "N09BGCSF", "BACK GRINDING", "", "AU", "2013-02-28" },
                    { 16, "11", "N09DDSSF", "DESIGN SERVICE", "", "AU", "2013-03-04" },
                    { 17, "11", "N09ENGSF", "ENGINEERRING", "", "AU", "2013-03-15" },
                    { 18, "11", "N09HHCSF", "HOLD", "", "AU", "2013-03-05" },
                    { 19, "11", "N09HOTSF", "HOT", "", "AU", "2013-03-04" },
                    { 20, "11", "N09LLCSF", "PROTOTYPE LOT", "", "AU", "2013-03-05" },
                    { 21, "11", "N09LMFSF", "MPW SHUTTLE", "", "AU", "2013-03-04" },
                    { 22, "11", "N09MHFSF", "HANDLING", "", "AU", "2013-03-05" },
                    { 23, "11", "N09MSKCS", "MASK", "", "AU", "2019-06-18" },
                    { 24, "11", "N09MSKSF", "MASK", "", "AU", "2013-02-28" },
                    { 25, "11", "N09PCBCS", "PROBE CARD", "", "AU", "2017-08-02" },
                    { 26, "11", "N09PRBCS", "PROBE CARD", "", "AU", "2017-08-02" },
                    { 27, "11", "N09PSCSF", "SPLIT", "", "AU", "2013-03-04" }
                });

            migrationBuilder.InsertData(
                table: "MealPrice",
                columns: new[] { "MealPriceID", "InsertName", "InsertOrgID", "InsertOrgNameEng", "InsertOrgNameKor", "Location", "Price", "UpdateDate", "UpdateIP", "UpdateSabun" },
                values: new object[,]
                {
                    { 1, null, null, null, null, "2000", 3500, null, null, null },
                    { 2, null, null, null, null, "3000", 3500, null, null, null },
                    { 3, null, null, null, null, "6000", 4000, null, null, null }
                });

            migrationBuilder.InsertData(
                table: "MealSchedule",
                columns: new[] { "MealScheduleID", "EndTime", "InsertName", "InsertOrgID", "InsertOrgNameEng", "InsertOrgNameKor", "MealGB", "StartTime", "Term", "UpdateDate", "UpdateIP", "UpdateSabun" },
                values: new object[,]
                {
                    { 1, "0900", null, null, null, null, 1, "0530", 0, null, "", null },
                    { 2, "1400", null, null, null, null, 2, "1130", 0, null, "", null },
                    { 3, "1900", null, null, null, null, 3, "1630", 0, null, "", null },
                    { 4, "2330", null, null, null, null, 4, "2130", 0, null, "", null },
                    { 5, "0900", null, null, null, null, 1, "0530", 1, null, "", null },
                    { 6, "1400", null, null, null, null, 2, "1130", 1, null, "", null },
                    { 7, "1900", null, null, null, null, 3, "1630", 1, null, "", null },
                    { 8, "2330", null, null, null, null, 4, "2130", 1, null, "", null }
                });

            migrationBuilder.InsertData(
                table: "MealTerm",
                columns: new[] { "MealTermID", "EndDate", "InsertName", "InsertOrgID", "InsertOrgNameEng", "InsertOrgNameKor", "StartDate", "Term", "UpdateDate", "UpdateIP", "UpdateSabun" },
                values: new object[,]
                {
                    { 1, "0831", null, null, null, null, "0401", 0, null, "", null },
                    { 2, "0331", null, null, null, null, "0901", 1, null, "", null }
                });

            migrationBuilder.InsertData(
                table: "Menu",
                columns: new[] { "MenuID", "Depth", "GroupNo", "IconImg", "InsertName", "InsertOrgID", "InsertOrgNameEng", "InsertOrgNameKor", "InsertSabun", "MenuNameEng", "MenuNameKor", "OrderNo", "URL", "UpdateDate" },
                values: new object[,]
                {
                    { 1, 0, 1, "/images/ico/ico-gnb-list01.svg", null, null, null, null, "1", "Person Info", "임직원 정보관리", 1, "", null },
                    { 2, 1, 1, null, null, null, null, null, "1", "Person Info", "임직원 관리", 2, "/person/list", null },
                    { 3, 1, 1, null, null, null, null, null, "1", "Card", "출입증 관리", 5, "/card/list", null },
                    { 4, 1, 1, null, null, null, null, null, "1", "Card Car", "차량 관리", 7, "/card/carlist", null },
                    { 5, 0, 5, "/images/ico/ico-gnb-list02.svg", null, null, null, null, "1", "Company", "업체정보관리", 1, "", null },
                    { 6, 1, 5, null, null, null, null, null, "1", "Company", "업체정보관리", 2, "/company/list", null },
                    { 7, 0, 7, "/images/ico/ico-gnb-list03.svg", null, null, null, null, "1", "Visit", "방문객관리", 1, "", null },
                    { 8, 1, 7, null, null, null, null, null, "1", "Visit", "방문객 관리", 2, "/visit/list", null },
                    { 9, 1, 7, null, null, null, null, null, "1", "CarryItem", "휴대물품 관리", 5, "/carryitem/list", null },
                    { 10, 1, 7, null, null, null, null, null, "1", "Visit Manual", "방문객수기입력", 8, "/visit/manuallist", null },
                    { 11, 0, 11, "/images/ico/ico-gnb-list04.svg", null, null, null, null, "1", "Card Temp", "임시증 관리", 1, "", null },
                    { 12, 1, 11, null, null, null, null, null, "1", "Card Temp", "임시증 관리", 2, "/card/templist", null },
                    { 13, 0, 13, "/images/ico/ico-gnb-list05.svg", null, null, null, null, "1", "ExportImport", "반출입 관리", 1, "", null },
                    { 14, 1, 13, null, null, null, null, null, "1", "ExportImport", "반출입관리", 2, "/exportimport/list", null },
                    { 15, 1, 13, null, null, null, null, null, "1", "ExportImport Notebook", "노트북자가결재", 5, "/exportimport/selfapproval", null },
                    { 16, 0, 16, "/images/ico/ico-gnb-list06.svg", null, null, null, null, "1", "Work / Safety", "공사/안전 관리", 1, "", null },
                    { 17, 1, 16, null, null, null, null, null, "1", "Work", "공사등록현황", 2, "/work/list", null },
                    { 18, 1, 16, null, null, null, null, null, "1", "Work Visit", "공사신청현황", 5, "/work/visitlist", null },
                    { 19, 1, 16, null, null, null, null, null, "1", "Safety Edu", "안전교육현황", 8, "/work/safetyedulist", null },
                    { 20, 1, 16, null, null, null, null, null, "1", "I/R", "I/R발행", 11, "/work/safetyviolationlist", null },
                    { 21, 1, 16, null, null, null, null, null, "1", "Statistics", "통계관리", 14, "/work/statisticslist", null },
                    { 22, 1, 16, null, null, null, null, null, "1", "Safety Violation", "안전위규관리", 15, "/work/safetyviolationreasonlist", null },
                    { 23, 0, 23, "/images/ico/ico-gnb-list07.svg", null, null, null, null, "1", "Meal", "식수집계관리", 1, "", null },
                    { 24, 1, 23, null, null, null, null, null, "1", "Meal", "식수현황조회", 2, "/meal/list", null },
                    { 25, 1, 23, null, null, null, null, null, "1", "Meal Invalid", "이상식수조회", 4, "/meal/invalidlist", null },
                    { 26, 1, 23, null, null, null, null, null, "1", "Meal Permission", "식수권한관리", 5, "/meal/permissionlist", null },
                    { 27, 1, 23, null, null, null, null, null, "1", "Meal Info", "식수정보관리", 6, "/meal/infolist", null },
                    { 28, 1, 23, null, null, null, null, null, "1", "Meal Manual", "식수수기등록", 7, "/meal/manuallist", null },
                    { 29, 0, 29, "/images/ico/ico-gnb-list08.svg", null, null, null, null, "1", "MyInfo", "내정보관리", 1, "", null },
                    { 30, 1, 29, null, null, null, null, null, "1", "MyInfo", "내정보관리", 2, "/person/myinfo", null },
                    { 31, 0, 31, "/images/ico/ico-gnb-list09.svg", null, null, null, null, "1", "Notice", "공지사항관리", 1, "", null },
                    { 32, 1, 31, null, null, null, null, null, "1", "Notice", "공지사항관리", 2, "/notice/list", null },
                    { 33, 0, 33, "/images/ico/ico-gnb-list10.svg", null, null, null, null, "1", "System Management", "시스템관리", 1, "", null },
                    { 34, 1, 33, null, null, null, null, null, "1", "Menu", "메뉴관리", 2, "/sys/menulist", null },
                    { 35, 1, 33, null, null, null, null, null, "1", "Level", "레벨관리", 3, "/sys/levellist", null },
                    { 36, 1, 33, null, null, null, null, null, "1", "Approval", "결재경로관리", 4, "/sys/approval", null },
                    { 37, 1, 33, null, null, null, null, null, "1", "CommonCode", "공통코드관리", 5, "/sys/commoncode", null },
                    { 38, 1, 33, null, null, null, null, null, "1", "Equipment", "장비관리", 6, "", null },
                    { 39, 1, 33, null, null, null, null, null, "1", "Holiday", "공휴일등록관리", 7, "/sys/holidaylist", null },
                    { 40, 0, 40, "/images/ico/ico-gnb-list11.svg", null, null, null, null, "1", "Access Record", "출입기록관리", 1, "", null },
                    { 41, 1, 40, null, null, null, null, null, "1", "Access Record Person", "인원출입조회", 2, "/accessrecord/personlist", null },
                    { 42, 1, 40, null, null, null, null, null, "1", "Access Record Car", "차량출입조회", 3, "/accessrecord/carlist", null },
                    { 43, 1, 40, null, null, null, null, null, "1", "Access Record", "인원출입현황", 4, "/accessrecord/statisticslist", null },
                    { 44, 0, 44, "/images/ico/ico-gnb-list12.svg", null, null, null, null, "1", "Card Apply", "인원출입관리", 1, "", null },
                    { 45, 1, 44, null, null, null, null, null, "1", "Card Apply Person", "인원출입증현황 ", 2, "/card/applylist", null },
                    { 46, 1, 44, null, null, null, null, null, "1", "Card Apply Car", "차량출입증현황", 5, "/card/carapplylist", null },
                    { 47, 1, 44, null, null, null, null, null, "1", "BlackList", "블랙리스트관리", 8, "/person/blacklist", null }
                });

            migrationBuilder.InsertData(
                table: "MenuLevel",
                columns: new[] { "MenuLevelID", "InsertName", "InsertOrgID", "InsertOrgNameEng", "InsertOrgNameKor", "InsertSabun", "LevelID", "MenuID", "MenuNameEng", "MenuNameKor", "UpdateDate" },
                values: new object[,]
                {
                    { 1, null, null, null, null, "1", 1, 2, "Person Info", "임직원 관리", null },
                    { 2, null, null, null, null, "1", 1, 3, "Card", "출입증 관리", null },
                    { 3, null, null, null, null, "1", 1, 4, "Card Car ", "차량 관리", null },
                    { 4, null, null, null, null, "1", 1, 6, "Company", "업체정보관리", null },
                    { 5, null, null, null, null, "1", 1, 8, "Visit ", "방문객 관리", null },
                    { 6, null, null, null, null, "1", 1, 9, "CarryItem ", "휴대물품 관리", null },
                    { 7, null, null, null, null, "1", 1, 10, "Visit Manual", "방문객수기입력", null },
                    { 8, null, null, null, null, "1", 1, 12, "Card Temp ", "임시증 관리", null },
                    { 9, null, null, null, null, "1", 1, 14, "ExportImport ", "반출입관리", null },
                    { 10, null, null, null, null, "1", 1, 15, "ExportImport Notebook ", "노트북자가결재", null },
                    { 11, null, null, null, null, "1", 1, 17, "Work", "공사등록현황", null },
                    { 12, null, null, null, null, "1", 1, 18, "Work Visit ", "공사신청현황", null },
                    { 13, null, null, null, null, "1", 1, 19, "Safety Edu", "안전교육현황", null },
                    { 14, null, null, null, null, "1", 1, 20, "I/R ", "I/R발행", null },
                    { 15, null, null, null, null, "1", 1, 21, "Statistics ", "통계관리", null },
                    { 16, null, null, null, null, "1", 1, 22, "Safety Violation ", "안전위규관리", null },
                    { 17, null, null, null, null, "1", 1, 24, "Meal", "식수현황조회", null },
                    { 18, null, null, null, null, "1", 1, 25, "Meal Invalid ", "이상식수조회", null },
                    { 19, null, null, null, null, "1", 1, 26, "Meal Permission ", "식수권한관리", null },
                    { 20, null, null, null, null, "1", 1, 27, "Meal Info ", "식수정보관리", null },
                    { 21, null, null, null, null, "1", 1, 28, "Meal Manual", "식수수기등록", null },
                    { 22, null, null, null, null, "1", 1, 30, "MyInfo ", "내정보관리", null },
                    { 23, null, null, null, null, "1", 1, 32, "Notice ", "공지사항", null },
                    { 24, null, null, null, null, "1", 1, 34, "Menu", "메뉴관리", null },
                    { 25, null, null, null, null, "1", 1, 35, "Level ", "레벨관리", null },
                    { 26, null, null, null, null, "1", 1, 36, "Approval", "결재경로관리", null },
                    { 27, null, null, null, null, "1", 1, 37, "CommonCode ", "공통코드관리", null },
                    { 28, null, null, null, null, "1", 1, 38, " ", "장비관리", null },
                    { 29, null, null, null, null, "1", 1, 39, "Holiday ", "공휴일등록관리", null },
                    { 30, null, null, null, null, "1", 1, 41, "Access Record Person ", "인원출입조회", null },
                    { 31, null, null, null, null, "1", 1, 42, "Access Record Car ", "차량출입조회", null },
                    { 32, null, null, null, null, "1", 1, 43, "Access Record", "인원출입현황", null },
                    { 33, null, null, null, null, "1", 1, 45, "Card Apply Person ", "인원출입증신청현황", null },
                    { 34, null, null, null, null, "1", 1, 46, "Card Apply Car ", "차량출입증승인현황", null },
                    { 35, null, null, null, null, "1", 1, 47, "BlackList ", "블랙리스트관리", null }
                });

            migrationBuilder.InsertData(
                table: "Person",
                columns: new[] { "PersonID", "BirthDate", "CarAllowCnt", "CarRegCnt", "CardCreateCnt", "CardID", "CardIssueStatus", "CardNo", "CleanEduLastDate", "CompanyID", "Email", "GradeID", "GradeName", "HomeAddr", "HomeDetailAddr", "HomeZipcode", "ImageData", "ImageDataHash", "ImmEndDate", "ImmStartDate", "ImmStatusCodeID", "InsertName", "InsertOrgID", "InsertOrgNameEng", "InsertOrgNameKor", "InsertSabun", "IsRecTempCard", "LevelCodeID", "Location", "Mobile", "Name", "Nationality", "OrgID", "OrgNameEng", "OrgNameKor", "PID", "PWUpdateDate", "Password", "PersonTypeID", "Sabun", "SafetyEduLastDate", "Tel", "TempCardRecDate", "TermsPrivarcyFileData", "TermsPrivarcyFileDataHash", "UpdateDate", "UpdateIP", "ValidDate", "VisitLastDate", "VisitorEduLastDate", "WorkTypeCodeID" },
                values: new object[] { 1, null, null, null, null, null, null, null, null, 0, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, "1", null, 1, null, null, "마스터관리자", null, null, "master", "마스터", null, null, "jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=", null, "master", null, null, null, null, null, null, null, null, null, null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Approval");

            migrationBuilder.DropTable(
                name: "ApprovalHistory");

            migrationBuilder.DropTable(
                name: "ApprovalLine");

            migrationBuilder.DropTable(
                name: "ApprovalLineHistory");

            migrationBuilder.DropTable(
                name: "BlackList");

            migrationBuilder.DropTable(
                name: "BlackListHistory");

            migrationBuilder.DropTable(
                name: "CarCardIssueApply");

            migrationBuilder.DropTable(
                name: "CarCardIssueApplyHistory");

            migrationBuilder.DropTable(
                name: "CardIssueApply");

            migrationBuilder.DropTable(
                name: "CardIssueApplyHistory");

            migrationBuilder.DropTable(
                name: "CarryItemApply");

            migrationBuilder.DropTable(
                name: "CarryItemApplyHistory");

            migrationBuilder.DropTable(
                name: "CarryItemInfo");

            migrationBuilder.DropTable(
                name: "CarryItemInfoHistory");

            migrationBuilder.DropTable(
                name: "CommonCode");

            migrationBuilder.DropTable(
                name: "CommonCodeHistory");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropTable(
                name: "CompanyHistory");

            migrationBuilder.DropTable(
                name: "ExportImport");

            migrationBuilder.DropTable(
                name: "ExportImportHistory");

            migrationBuilder.DropTable(
                name: "ExportImportItem");

            migrationBuilder.DropTable(
                name: "Holiday");

            migrationBuilder.DropTable(
                name: "Level");

            migrationBuilder.DropTable(
                name: "LevelHistory");

            migrationBuilder.DropTable(
                name: "Material");

            migrationBuilder.DropTable(
                name: "MealAccess");

            migrationBuilder.DropTable(
                name: "MealLog");

            migrationBuilder.DropTable(
                name: "MealPrice");

            migrationBuilder.DropTable(
                name: "MealSchedule");

            migrationBuilder.DropTable(
                name: "MealTerm");

            migrationBuilder.DropTable(
                name: "Menu");

            migrationBuilder.DropTable(
                name: "MenuHistory");

            migrationBuilder.DropTable(
                name: "MenuLevel");

            migrationBuilder.DropTable(
                name: "MenuLevelHistory");

            migrationBuilder.DropTable(
                name: "NotebookSelfApproval");

            migrationBuilder.DropTable(
                name: "NotebookSelfApprovalHistory");

            migrationBuilder.DropTable(
                name: "Notice");

            migrationBuilder.DropTable(
                name: "PasswordHistory");

            migrationBuilder.DropTable(
                name: "Person");

            migrationBuilder.DropTable(
                name: "SafetyEdu");

            migrationBuilder.DropTable(
                name: "SafetyEduApply");

            migrationBuilder.DropTable(
                name: "SafetyEduApplyHistory");

            migrationBuilder.DropTable(
                name: "SafetyEduHistory");

            migrationBuilder.DropTable(
                name: "SafetyViolation");

            migrationBuilder.DropTable(
                name: "SafetyViolationHistory");

            migrationBuilder.DropTable(
                name: "SafetyViolationPerson");

            migrationBuilder.DropTable(
                name: "SafetyViolationPersonHistory");

            migrationBuilder.DropTable(
                name: "SafetyViolationReason");

            migrationBuilder.DropTable(
                name: "SafetyViolationReasonHistory");

            migrationBuilder.DropTable(
                name: "SMS");

            migrationBuilder.DropTable(
                name: "TempCardIssueApply");

            migrationBuilder.DropTable(
                name: "TempCardIssueApplyHistory");

            migrationBuilder.DropTable(
                name: "VisitApply");

            migrationBuilder.DropTable(
                name: "VisitApplyHistory");

            migrationBuilder.DropTable(
                name: "VisitApplyPerson");

            migrationBuilder.DropTable(
                name: "VisitApplyPersonHistory");

            migrationBuilder.DropTable(
                name: "VisitPerson");

            migrationBuilder.DropTable(
                name: "WorkApply");

            migrationBuilder.DropTable(
                name: "WorkApplyAttachFile");

            migrationBuilder.DropTable(
                name: "WorkApplyHistory");

            migrationBuilder.DropTable(
                name: "WorkVisitApply");

            migrationBuilder.DropTable(
                name: "WorkVisitApplyHistory");

            migrationBuilder.DropTable(
                name: "WorkVisitPersonApply");

            migrationBuilder.DropTable(
                name: "WorkVisitPersonApplyHistory");
        }
    }
}
