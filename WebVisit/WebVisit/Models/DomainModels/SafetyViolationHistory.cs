using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace WebVisit.Models
{
    public class SafetyViolationHistory
    {
        /* 
        req: SafetyViolationHistoryID, SafetyViolationID, ViolationDate, IsWorkOrder, ContactSabun, Location, SafetyViolationReasonID, SafetyViolationStatus
        no req: WorkOrderNo, ContactOrgID, ContactOrgNameKor, ContactOrgNameEng, ViolationLocation, DetailReason, StatementFile

        [common]
        req: UpdateSabun, InsertUpdateDate
        no req: UpdateOrgID, UpdateOrgNameKor, UpdateOrgNameEng 
        */
        [Key]
        public int SafetyViolationHistoryID { get; set; }
        public int SafetyViolationID { get; set; }
        
        // [Column(TypeName = "char(10)")]
        // public string ViolationDate { get; set; } = string.Empty;
        public DateTime ViolationDate { get; set; } 

        [MaxLength(100)]
        public string? WorkOrderNo { get; set; } = string.Empty;

        [Column(TypeName = "char(1)")]
        public string IsWorkOrder { get; set; } = string.Empty;


        //담당자: 사번, 부서ID, 부서명
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

        [MaxLength(50)]
        public string Location { get; set; } = string.Empty;
        // public int Location { get; set; }

        [MaxLength(100)]
        public string? ViolationLocation { get; set; } = string.Empty;
        
        public int SafetyViolationReasonID { get; set; }

        [MaxLength(300)]
        public string? DetailReason { get; set; } = string.Empty;
        public int SafetyViolationStatus { get; set; }


        [MaxLength(1000)]	
        public string? StatementFileData { get; set; } = string.Empty;

        [Column(TypeName = "varbinary(max)")]
        public byte[]? StatementFileDataHash {get; set;}



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

        public SafetyViolationHistory(){
            //
        }
        

    }
}
