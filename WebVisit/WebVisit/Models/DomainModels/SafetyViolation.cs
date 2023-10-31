using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace WebVisit.Models
{
    public class SafetyViolation
    {
        /* 
        req: SafetyViolationID, ViolationDate, IsWorkOrder, ContactSabun, Location, SafetyViolationReasonID, SafetyViolationStatus
        no req: WorkOrderNo, ContactOrgID, ContactOrgNameKor, ContactOrgNameEng, ViolationLocation, DetailReason, StatementFile

        [common]
        req: InsertSabun, InsertDate, IsDelete
        no req: InsertOrgID, InsertOrgNameKor, InsertOrgNameEng, UpdateDate, 
        */
        [Key]
        public int SafetyViolationID { get; set; }
        
        // [Column(TypeName = "char(10)")]
        // public string ViolationDate { get; set; } = string.Empty;
        public DateTime ViolationDate { get; set; } 

        [MaxLength(100)]
        public string? WorkOrderNo { get; set; } = string.Empty;

        [Column(TypeName = "char(1)")]
        public string IsWorkOrder { get; set; } = "N";


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

        [NotMapped]
        public List<IFormFile>? FormFile { get; set; }



        //발급자(등록자): 사번, 이름, 부서ID, 부서명
        [Required]
        [MaxLength(50)]
        public string InsertSabun { get; set; } = string.Empty;

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

        public SafetyViolation(){
            //
        }

        public SafetyViolation Clone() => (SafetyViolation)MemberwiseClone();
        

    }
}
