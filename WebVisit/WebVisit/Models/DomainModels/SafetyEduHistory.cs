using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace WebVisit.Models
{
    public class SafetyEduHistory
    {
        /* 
        req: SafetyEduHistoryID, SafetyEduID, EduDate, EduApplyStatus
        no req: WorkApplyID

        [common]
        req: UpdateSabun, InsertUpdateDate
        no req: UpdateOrgID, UpdateOrgNameKor, UpdateOrgNameEng 
        */
        [Key]
        public int SafetyEduHistoryID { get; set; }
        public int SafetyEduID { get; set; }

        [Column(TypeName = "char(10)")]
        public string EduDate { get; set; } = string.Empty;
        
        public int? WorkApplyID { get; set; }
        public int EduApplyStatus { get; set; }



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

        public SafetyEduHistory(){
            //
        }
        

    }
}
