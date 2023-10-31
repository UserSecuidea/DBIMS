using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace WebVisit.Models
{
    public class WorkVisitPersonApply
    {
        /* 
        req: WorkVisitPersonApplyID, WorkVisitApplyID, Sabun
        no req: OrgID, OrgNameKor, OrgNameEng

        [common]
        req: InsertDate, IsDelete
        no req: UpdateDate, 
        */
        [Key]
        public int WorkVisitPersonApplyID { get; set; }
        public int WorkVisitApplyID { get; set; }
        
        public int WorkVisitApplyStatus { get; set; }

        //출입자: 사번, 이름, 부서ID, 부서명
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


        //갱신일, 생성일, 삭제여부 
        public DateTime? UpdateDate { get; set; } 

        public DateTime InsertDate { get; set; } 
        
        [Column(TypeName = "char(1)")]
        public string IsDelete { get; set; } = "N";

        public WorkVisitPersonApply(){
            //
        }
        public WorkVisitPersonApply Clone() => (WorkVisitPersonApply)MemberwiseClone();
    }
}