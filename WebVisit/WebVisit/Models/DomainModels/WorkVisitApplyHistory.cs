using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace WebVisit.Models
{
    public class WorkVisitApplyHistory
    {
        /* 
        req: WorkVisitApplyHistoryID, WorkVisitApplyID, WorkApplyID, WorkDate
        no req: 

        [common]
        req: UpdateSabun, InsertUpdateDate
        no req: UpdateOrgID, UpdateOrgNameKor, UpdateOrgNameEng
        */
        [Key]
        public int WorkVisitApplyHistoryID { get; set; }
        public int WorkVisitApplyID { get; set; }
        public int WorkApplyID { get; set; }

        [Column(TypeName = "char(10)")]
        public string WorkDate { get; set; } = string.Empty;
        public int WorkVisitApplyStatus { get; set; }


        
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

        public WorkVisitApplyHistory(){
            //
        }
        

    }
}
