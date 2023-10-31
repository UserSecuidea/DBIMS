using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace WebVisit.Models
{
    public class SafetyEduApplyHistory
    {
        /*
        req: SafetyEduApplyHistoryID, SafetyEduApplyID, SafetyEduID, EduCompleteStatus, EduDate, Sabun
        no req: ValidDate, OrgID, OrgNameKor, OrgNameEng 
                
        [common]
        req: InsertUpdateDate
        no req:  
        */
        [Key]
        public int SafetyEduApplyHistoryID { get; set; }
        public int SafetyEduApplyID { get; set; }
        public int SafetyEduID { get; set; }
        public int EduCompleteStatus { get; set; }
        public DateTime EduDate { get; set; } 
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



        //생성(갱신)일
        public DateTime InsertUpdateDate { get; set; } 

        public SafetyEduApplyHistory(){
            //
        }
    }
}
