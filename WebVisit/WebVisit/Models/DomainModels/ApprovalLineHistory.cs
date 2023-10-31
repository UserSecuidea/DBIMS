using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace WebVisit.Models
{
    public class ApprovalLineHistory
    {
        /* req: ApprovalLineHistoryID, ApprovalLineID, ApprovalID, ApprovalSysType, ApprovalSabun
        no req: ApprovalOrgID, ApprovalOrgNameKor, ApprovalOrgNameEng  

        [common]
        req: UpdateSabun, InsertUpdateDate
        no req: UpdateOrgID, UpdateOrgNameKor, UpdateOrgNameEng
        */
        [Key]
        public int ApprovalLineHistoryID { get; set; }
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

        public ApprovalLineHistory(){
            //
        }

    }
}
