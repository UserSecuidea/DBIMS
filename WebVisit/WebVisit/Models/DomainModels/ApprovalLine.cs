using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace WebVisit.Models
{
    public class ApprovalLine
    {
        /* req: ApprovalLineID, ApprovalID, ApprovalSysType, ApprovalSabun
        no req: ApprovalOrgID, ApprovalOrgNameKor, ApprovalOrgNameEng  

        [common]
        req: InsertSabun, InsertDate, IsDelete
        no req: InsertOrgID, InsertOrgNameKor, InsertOrgNameEng, UpdateDate,
        */
        [Key]
        public int ApprovalLineID { get; set; }
        public int ApprovalID { get; set; }
        public int ApprovalSysType { get; set; }

        //결재자: 사번, 이름, 부서ID, 부서명
        [Required]
        [MaxLength(50)]
        public string ApprovalSabun { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? ApprovalName { get; set; } = string.Empty;

        // public int? ApprovalOrgID {get; set;}
        [MaxLength(50)]
        public string? ApprovalOrgID { get; set; } = string.Empty;
        

        [MaxLength(100)]
        public string? ApprovalOrgNameKor { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? ApprovalOrgNameEng { get; set; } = string.Empty;



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
        public string IsDelete { get; set; }  = "N";


        public ApprovalLine(){
            //
        }
        public ApprovalLine Clone() => (ApprovalLine)MemberwiseClone();

    }
}
