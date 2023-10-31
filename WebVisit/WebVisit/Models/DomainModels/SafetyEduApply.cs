using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace WebVisit.Models
{
    public class SafetyEduApply
    {
        /*
        req: SafetyEduApplyID, SafetyEduID, EduCompleteStatus, EduDate, Sabun
        no req: ValidDate, OrgID, OrgNameKor, OrgNameEng 
                
        [common]
        req: InsertDate, IsDelete
        no req: UpdateDate, 
        */
        [Key]
        public int SafetyEduApplyID { get; set; }
        public int SafetyEduID { get; set; }
        public int EduCompleteStatus { get; set; }
        public DateTime? EduDate { get; set; } 
        public DateTime? ValidDate { get; set; } 




        //교육받는자: 사번, 이름, 부서ID, 부서명
        [Required]
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



        //갱신일, 생성일, 삭제여부 
        public DateTime? UpdateDate { get; set; } 

        public DateTime InsertDate { get; set; } 
        
        [Column(TypeName = "char(1)")]
        public string IsDelete { get; set; } = "N";

        public SafetyEduApply(){
            //
        }

        public SafetyEduApply Clone() => (SafetyEduApply)MemberwiseClone();
    }
}
