using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace WebVisit.Models
{
    public class WorkApply
    {
        /* 
        req: WorkApplyID, Location, ContactSabun, WorkName, WorkStartDate, WorkEndDate, WorkContractFile, WorkApplyStatus, IsWorkReg
        no req: ContactOrgID, ContactOrgNameKor, ContactOrgNameEng, WorkMemo

        [common]
        req: InsertSabun, InsertDate, IsDelete
        no req: InsertOrgID, InsertOrgNameKor, InsertOrgNameEng, UpdateDate, 
        */
        [Key]
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

        [NotMapped]
        public List<IFormFile>? WorkContractFormFile { get; set; }

        [NotMapped]
        public List<IFormFile>? FormFile1 { get; set; }

        [NotMapped]
        public List<IFormFile>? FormFile2 { get; set; }

        [NotMapped]
        public List<IFormFile>? FormFile3 { get; set; }

        [NotMapped]
        public List<IFormFile>? FormFile4 { get; set; }

        [NotMapped]
        public List<IFormFile>? FormFile5 { get; set; }

        [NotMapped]
        public List<IFormFile>? FormFile6 { get; set; }

        [NotMapped]
        public List<IFormFile>? FormFile7 { get; set; }

        [NotMapped]
        public List<IFormFile>? FormFile8 { get; set; }

        [NotMapped]
        public List<IFormFile>? FormFile9 { get; set; }

        [NotMapped]
        public List<IFormFile>? FormFile10 { get; set; }

        [NotMapped]
        public List<IFormFile>? FormFile11 { get; set; }

        [NotMapped]
        public List<IFormFile>? FormFile12 { get; set; }

        [NotMapped]
        public List<IFormFile>? FormFile13 { get; set; }

        public int WorkApplyStatus { get; set; }

        
        [Column(TypeName = "char(1)")]
        public string IsWorkReg { get; set; } = "N";



        //등록자: 사번, 이름, 부서ID, 부서명
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

        public WorkApply(){
            //
        }

        public WorkApply Clone() => (WorkApply)MemberwiseClone();     

    }
}
