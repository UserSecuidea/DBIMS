using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace WebVisit.Models
{
    public class WorkApplyHistory
    {
        /* 
        req: WorkApplyHistoryID, WorkApplyID, Location, ContactSabun, WorkName, WorkStartDate, WorkEndDate, WorkContractFile, WorkApplyStatus, IsWorkReg
        no req: ContactOrgID, ContactOrgNameKor, ContactOrgNameEng, WorkMemo, RejectReason

        [common]
        req: UpdateSabun, InsertUpdateDate
        no req: UpdateOrgID, UpdateOrgNameKor, UpdateOrgNameEng
        */
        [Key]
        public int WorkApplyHistoryID { get; set; }
        public int WorkApplyID { get; set; }
        
        [MaxLength(20)]
        public string WorkApplyNo { get; set; } = string.Empty; //P202306250002 
        
        [MaxLength(50)]
        public string Location { get; set; } = string.Empty;
        // public int Location { get; set; }


        //담당자: 사번, 이름, 부서ID, 부서명
        [Required]
        [MaxLength(50)]
        public string ContactSabun { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? ContactName { get; set; } = string.Empty;

        // public int? ContactOrgID {get; set;}
        [MaxLength(50)]
        public string? ContactOrgID { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? ContactOrgNameKor { get; set; } = string.Empty;
        [MaxLength(100)]
        public string? ContactOrgNameEng { get; set; } = string.Empty;

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

        

        [MaxLength(200)]
        public string WorkName { get; set; } = string.Empty;
        [MaxLength(500)]
        public string? WorkMemo { get; set; } = string.Empty;
       
        [Column(TypeName = "char(10)")]
        public string WorkStartDate { get; set; } = string.Empty;
        
        [Column(TypeName = "char(10)")]
        public string WorkEndDate { get; set; } = string.Empty;

        
        [MaxLength(1000)]	
        public string? WorkContractFileData { get; set; } = string.Empty;

        [Column(TypeName = "varbinary(max)")]
        public byte[]? WorkContractFileDataHash {get; set;}

        public int WorkApplyStatus { get; set; }

        [Column(TypeName = "char(1)")]
        public string IsWorkReg { get; set; } = string.Empty;

        [MaxLength(300)]
        public string? RejectReason { get; set; } = string.Empty;



        //등록&변경자: 사번, 이름, 부서ID, 부서명
        [Required]
        [MaxLength(50)]
        public string UpdateSabun { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? UpdateName { get; set; } = string.Empty;

        // public int? UpdateOrgID {get; set;}
        [MaxLength(50)]
        public string? UpdateOrgID { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? UpdateOrgNameKor { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? UpdateOrgNameEng { get; set; } = string.Empty;

        //생성(갱신)일
        public DateTime InsertUpdateDate { get; set; } 

        public WorkApplyHistory(){
            //
        }
        

    }
}
