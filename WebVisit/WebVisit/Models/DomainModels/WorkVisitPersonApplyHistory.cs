using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace WebVisit.Models
{
    public class WorkVisitPersonApplyHistory
    {
        /* 
        req: WorkVisitPersonApplyHistoryID, WorkVisitPersonApplyID, WorkVisitApplyID, Sabun
        no req: OrgID, OrgNameKor, OrgNameEng 

        [common]
        req: InsertUpdateDate
        no req: 
        */
        [Key]
        public int WorkVisitPersonApplyHistoryID { get; set; }
        public int WorkVisitPersonApplyID { get; set; }
        public int WorkVisitApplyID { get; set; }
        
        public int WorkVisitApplyStatus { get; set; }

        //사원: 사번, 이름, 부서ID, 부서명
        [Required]
        [MaxLength(50)]
        public string Sabun { get; set; } = string.Empty;

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
        

        public WorkVisitPersonApplyHistory(){
            //
        }
        

    }
}