using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace WebVisit.Models
{
    public class CarryItemApply
    {
        /* 
        req: CarryItemApplyID, ImportDate, Location, ImportPurposeCodeID, PlaceCodeID, ImportWayType, CarryVisitorType, CarryItemApplyStatus, CarryItemStatus, InsertVisitorType
        no req: VisitApplyPersonID, ImportDate, ImportReason, VisitPersonID, Sabun, OrgID, OrgNameKor, OrgNameEng, ApprovalSabun, ApprovalOrgID, ApprovalOrgNameKor, ApprovalOrgNameEng, ContactSabun, ContactOrgID, ContactOrgNameKor, ContactOrgNameEng, ApprovalOpinion, ApprovalDate
        InsertVisitPersonID, 

        [common]
        req: InsertDate, IsDelete
        no req: InsertSabun, InsertOrgID, InsertOrgNameKor, InsertOrgNameEng, UpdateDate, 
        */
        [Key]
        public int CarryItemApplyID { get; set; }
        // public int? VisitApplyPersonID { get; set; }
        
        [Column(TypeName = "char(10)")]
        public string? ImportDate { get; set; } = string.Empty;
        
        [Column(TypeName = "char(10)")]
        public string? ExportDate { get; set; } = string.Empty;
        
        public DateTime? EntryDate { get; set; } 
        public DateTime? ExitDate { get; set; } 
        
        [MaxLength(50)]
        public string Location { get; set; } = string.Empty;
        // public int Location { get; set; }

        public int ImportPurposeCodeID { get; set; }
        public int PlaceCodeID { get; set; }
        
        [MaxLength(300)]
        public string? ImportReason { get; set; } = string.Empty; // 사용 목적 못찾음

        public int ImportWayType { get; set; } // (사용안함) 구 통문 컬럼 - 반입자구분: 대행등록(0,임직원신청)/본인(1,방문신청)
        public int CarryVisitorType { get; set; } // 방문자구분: 임직원(0)/방문자(1)
        public int? VisitPersonID { get; set; }

        // 반입자: 사번, 이름, 부서ID, 부서명, 회사명, 휴대전화 
        // [Required] * 반입자가 방문자회원 일 경우 Sabun 대신에 VisitPersonID 가 들어감. 
        [MaxLength(50)]
        public string? Sabun { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? Name { get; set; } = string.Empty;
        
        // public int? OrgID {get; set;} // 방문객의 경우 없는 경우가 많음
        [MaxLength(50)]
        public string? OrgID { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? OrgNameKor { get; set; } = string.Empty; // 방문객의 경우 없는 경우가 많음

        [MaxLength(100)]
        public string? OrgNameEng { get; set; } = string.Empty; // 방문객의 경우 없는 경우가 많음

        [MaxLength(50)]
        public string? Mobile { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? CompanyName { get; set; } = string.Empty;

        [NotMapped]
        public string? GradeName { get; set; } = string.Empty; // 노트북 MySQL DB에 입력하기 위한 정보
        
        [NotMapped]
        public string? Tel { get; set; } = string.Empty; // 노트북 MySQL DB에 입력하기 위한 정보

        //결재자: 사번, 이름, 부서ID, 부서명
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

        //담당자: 사번, 이름, 부서ID, 부서명, 휴대전화 
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
        public string? ContactMobile { get; set; } = string.Empty;

        //휴대물품신청상태: 승인대기(0)/승인완료(1)/반려(2)/결재중(3)/결재완료(4)
        public int CarryItemApplyStatus { get; set; }
        //휴대물품반입반출상태: 반입대기(0)/반입완료(1)/확인대기(2)/확인완료(3)/반출대기(4)/반출완료(5)/미반입(6)/미반출(7)
        public int CarryItemStatus { get; set; }
        //휴대물품반입구분: 0 반입대기, 1 반입처리, 2 반출요, 3 반출불가, 4 반출불필요(유상), 5 반출불필요(무상)
        public int? CarryItemImportType { get; set; }

        

        [MaxLength(300)]
        public string? ApprovalOpinion { get; set; } = string.Empty;
        public DateTime? ApprovalDate { get; set; } 
        
        [Column(TypeName = "char(5)")]
        public string? ImportTime { get; set; } = string.Empty;
        
        [Column(TypeName = "char(5)")]
        public string? ExportTime { get; set; } = string.Empty;

        public int InsertVisitorType { get; set; }
        public int? InsertVisitPersonID { get; set; }

        //등록자: 사번, 이름, 부서ID, 부서명
        // [Required] * 등록자가 방문자회원 일 경우 InsertSabun 대신에 InsertVisitPersonID 가 들어감. 
        [Required]
        [MaxLength(50)]
        public string? InsertSabun { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? InsertName { get; set; } = string.Empty;

        // public int? InsertOrgID {get; set;}
        [MaxLength(50)]
        public string? InsertOrgID { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? InsertOrgNameKor { get; set; } = string.Empty;
        [MaxLength(100)]
        public string? InsertOrgNameEng { get; set; } = string.Empty;

        //갱신일, 생성일, 삭제여부 
        public DateTime? UpdateDate { get; set; } 

        public DateTime InsertDate { get; set; } 
        
        [Column(TypeName = "char(1)")]
        public string IsDelete { get; set; } = "N";
/*
        [Column(TypeName = "char(1)")]
        public string IsVaccineUpdated { get; set; } = "N";

        [Column(TypeName = "char(1)")]
        public string IsVirusScanned { get; set; } = "N";

        [Column(TypeName = "char(1)")]
        public string QuarantineConfirmationGate { get; set; } = "N";

        [Column(TypeName = "char(1)")]
        public string QuarantineConfirmationContact { get; set; } = "N";
*/
        public CarryItemApply(){
            //
        }
        public CarryItemApply Clone() => (CarryItemApply)MemberwiseClone();
    }
}

