using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace WebVisit.Models
{
    public class ExportImportHistory
    {
        /* 
        req: ExportImportHistoryID, ExportImportID, ExportImportType, ExportType, IsSelf, ExportImportStatus, IsSelfApproval
        no req: ExportLocation, ImportLocation, ExportImportPurposeType, ImportType, ExportDate, ImportDate, DeliveryMethodType, Reason, isVMI
        CompanySabun, CompanyName, BizRegNo, Tel, Sabun, Name, OrgID, OrgNameKor, OrgNameEng, ApprovalSabun, ApprovalName, ApprovalOrgID, ApprovalOrgNameKor, ApprovalOrgNameEng, ContactSabun, ContactName, ContactOrgID, ContactOrgNameKor, ContactOrgNameEng 
        ManagementNo, SerialNo, CeoApprovalFile, CheckCnt, Opinion
        
        [common]
        req: InsertUpdateDate
        no req: UpdateSabun, UpdateOrgID, UpdateOrgNameKor, UpdateOrgNameEng 
        */
        [Key]
        public int ExportImportHistoryID { get; set; }
        public int ExportImportID { get; set; }
        
        [MaxLength(20)]
        public string ExportImportNo { get; set; } = string.Empty; //C202306250002 

        public int ExportImportType { get; set; }
        public int ExportType { get; set; }
        
        
        [MaxLength(50)]
        public string? ExportLocation { get; set; } = string.Empty;
        
        [MaxLength(50)]
        public string? ImportLocation { get; set; } = string.Empty;
        
        public int? ExportImportPurposeType { get; set; }
        public int? ImportType { get; set; }
        
        [Column(TypeName = "char(10)")]
        public string? ImportDate { get; set; } = string.Empty;
        
        [Column(TypeName = "char(10)")]
        public string? ExportDate { get; set; } = string.Empty;

        public int? DeliveryMethodType { get; set; }
        
        [MaxLength(2000)]
        public string? Reason { get; set; } = string.Empty;

        [Column(TypeName = "char(1)")]
        public string? IsVMI { get; set; } = string.Empty;


        //외부업체인수자: 사번, 담당자이름, 회사명, 사업자번호, 연락처
        [MaxLength(50)]
        public string? CompanySabun { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? CompanyContactName { get; set; } = string.Empty;
        [MaxLength(100)]
        public string? CompanyName { get; set; } = string.Empty;
        [MaxLength(50)]
        public string? BizRegNo { get; set; } = string.Empty;
        
        [MaxLength(50)]
        public string? CompanyTel { get; set; } = string.Empty;

        [Column(TypeName = "char(1)")]
        public string? IsSelf { get; set; } = string.Empty;
        


        //반입사업장인수자: 사번, 성명, 부서ID, 부서명, 연락처 
        [MaxLength(50)]
        public string? Sabun { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? Name { get; set; } = string.Empty;
        
        // public int? OrgID {get; set;}
        [MaxLength(50)]
        public string? OrgID { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? OrgNameKor { get; set; } = string.Empty;
        [MaxLength(100)]
        public string? OrgNameEng { get; set; } = string.Empty;
        
        [MaxLength(50)]
        public string? Tel { get; set; } = string.Empty;


        //결재자: 사번, 성명, 부서ID, 부서명, 연락처 
        [MaxLength(50)]
        public string? ApprovalSabun { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? ApprovalName { get; set; } = string.Empty;

        // public int? ApprovalOrgID {get; set;}
        [MaxLength(50)]
        public string? ApprovalOrgID { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? ApprovalOrgNameKor { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? ApprovalOrgNameEng { get; set; } = string.Empty;
        
        [MaxLength(50)]
        public string? ApprovalTel { get; set; } = string.Empty;


        //파트장: 사번, 성명, 부서ID, 부서명, 연락처 2023.10.06 파트장 추가 
        [MaxLength(50)]
        public string? ManagerSabun { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? ManagerName { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? ManagerOrgID { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? ManagerOrgNameKor { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? ManagerOrgNameEng { get; set; } = string.Empty;
        
        [MaxLength(50)]
        public string? ManagerTel { get; set; } = string.Empty;


        //담당자: 사번, 성명, 부서ID, 부서명, 연락처 
        [MaxLength(50)]
        public string? ContactSabun { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? ContactName { get; set; } = string.Empty;

        // public int? ContactOrgID {get; set;}
        [MaxLength(50)]
        public string? ContactOrgID { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? ContactOrgNameKor { get; set; } = string.Empty;
        [MaxLength(100)]
        public string? ContactOrgNameEng { get; set; } = string.Empty;
        
        [MaxLength(50)]
        public string? ContactTel { get; set; } = string.Empty;


        //반출입신청상태: 승인대기(0)/승인완료(1)/반려(2)/결재중(3)/결재완료(4) // 2023.09.13 반출입상태에서 반출입신청상태 코드 분리
        public int ExportImportApplyStatus { get; set; }
        //반출입상태: 반출대기(4), 반출확인(5), 정문반송(6), 반출완료(7), 반입상신(8), 반입대기(9), 반입확인(10), 반입완료(11)
        public int ExportImportStatus { get; set; }


        [Column(TypeName = "char(2)")]
        public string? ExportDateHour { get; set; } = string.Empty;

        [Column(TypeName = "char(2)")]
        public string? ExportDateMinute { get; set; } = string.Empty;

        [Column(TypeName = "char(2)")]
        public string? ImportDateHour { get; set; } = string.Empty;

        [Column(TypeName = "char(2)")]
        public string? ImportDateMinute { get; set; } = string.Empty;


        [MaxLength(100)]
        public string? ManagementNo { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? SerialNo { get; set; } = string.Empty;

        [Column(TypeName = "char(1)")]
        public string? IsSelfApproval { get; set; }


        [MaxLength(1000)]
        public string? CeoApprovalFileData { get; set; } = string.Empty;

        [Column(TypeName = "varbinary(max)")]
        public byte[]? CeoApprovalFileDataHash {get; set;}




        [MaxLength(300)]
        public string? Opinion { get; set; } = string.Empty;
        public int? CheckCnt { get; set; }

        
        //등록&변경자: 사번, 이름, 부서ID, 부서명
        [Required]
        [MaxLength(50)]
        public string? UpdateSabun { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? UpdateName { get; set; } = string.Empty;

        // public int? UpdateOrgID {get; set;}
        [MaxLength(50)]
        public string? UpdateOrgID { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? UpdateOrgNameKor { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? UpdateOrgNameEng { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? UpdateGradeName { get; set; } = string.Empty;

        

        //생성(갱신)일
        public DateTime InsertUpdateDate { get; set; } 


        public ExportImportHistory(){
            //
        }
        

    }
}

