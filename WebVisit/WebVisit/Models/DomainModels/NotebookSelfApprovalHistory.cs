using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace WebVisit.Models
{
    public class NotebookSelfApprovalHistory
    {
        /* 
        req: NotebookSelfApprovalHistoryID, NotebookSelfApprovalID, Sabun
        no req: Name, OrgID, OrgNameKor, OrgNameEng, GradeID, GradeName

        [common]
        req: InsertDate, IsDelete
        no req: InsertSabun, InsertOrgID, InsertOrgNameKor, InsertOrgNameEng, UpdateDate, 
        */
        [Key]
        public int NotebookSelfApprovalHistoryID { get; set; }
        public int NotebookSelfApprovalID { get; set; }
        
        [MaxLength(50)]
        public string? Location { get; set; } = string.Empty; // 사업장
        
        [MaxLength(100)]
        public string? CompanyName { get; set; } = string.Empty; // 회사명  


        //자가결재자: 사번, 성명, 부서ID, 부서명
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

        public int? GradeID {get; set;}

        [MaxLength(100)]
        public string? GradeName { get; set; } = string.Empty;



        //등록&변경자: 사번, 이름, 부서ID, 부서명
        [Required]
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

        //생성(갱신)일
        public DateTime InsertUpdateDate { get; set; } 


        public NotebookSelfApprovalHistory(){
            //
        }
        

    }
}

