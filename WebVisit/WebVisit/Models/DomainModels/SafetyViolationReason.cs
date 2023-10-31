using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace WebVisit.Models
{
    public class SafetyViolationReason
    {
        /* 
        req: SafetyViolationReasonID, SafetyViolationReasonContents, ViolationPenaltyPeoriod1, IsActive
        no req: ViolationPenaltyPeoriod2, ViolationPenaltyPeoriod3, ViolationLevel, OrderNo

        [common]
        req: InsertSabun, InsertDate, IsDelete
        no req: InsertOrgID, InsertOrgNameKor, InsertOrgNameEng, UpdateSabun, UpdateOrgID, UpdateOrgNameKor, UpdateOrgNameEng, UpdateDate, 
        */
        [Key]
        public int SafetyViolationReasonID { get; set; }

        [MaxLength(300)]
        public string SafetyViolationReasonContents { get; set; } = string.Empty;

        public int ViolationPenaltyPeoriod1 { get; set; }
        public int? ViolationPenaltyPeoriod2 { get; set; }
        public int? ViolationPenaltyPeoriod3 { get; set; }
        public int? ViolationLevel { get; set; }
        public int? OrderNo { get; set; }

        [Column(TypeName = "char(1)")]
        public string IsActive { get; set; }




        //최초등록자: 사번, 이름, 부서ID, 부서명
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


        //최종수정자: 사번, 이름, 부서ID, 부서명
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

        //갱신일, 생성일, 삭제여부 
        public DateTime? UpdateDate { get; set; } 

        public DateTime InsertDate { get; set; } 
        
        [Column(TypeName = "char(1)")]
        public string IsDelete { get; set; } = "N";

        public SafetyViolationReason(){
            //
        }


        public SafetyViolationReason Clone() => (SafetyViolationReason)MemberwiseClone();
        

    }
}
