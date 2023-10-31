using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace WebVisit.Models
{
    public class SafetyViolationReasonHistory
    {
        /* 
        req: SafetyViolationReasonHistoryID, SafetyViolationReasonID, SafetyViolationReasonContents, ViolationPenaltyPeoriod1, IsActive
        no req: ViolationPenaltyPeoriod2, ViolationPenaltyPeoriod3, ViolationLevel, OrderNo

        [common]
        req: UpdateSabun, InsertUpdateDate
        no req: UpdateOrgID, UpdateOrgNameKor, UpdateOrgNameEng 
        */
        [Key]
        public int SafetyViolationReasonHistoryID { get; set; }
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

        public SafetyViolationReasonHistory(){
            //
        }
        

    }
}
