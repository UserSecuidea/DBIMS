using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace WebVisit.Models
{
    public class NotebookSelfApproval
    {
        /* 
        req: NotebookSelfApprovalID, Sabun
        no req: Name, OrgID, OrgNameKor, OrgNameEng, GradeID, GradeName

        [common]
        req: InsertDate, IsDelete
        no req: InsertSabun, InsertOrgID, InsertOrgNameKor, InsertOrgNameEng, UpdateDate, 
        */
        [Key]
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



        //등록자: 사번, 이름, 부서ID, 부서명
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


        //갱신일, 생성일, 삭제여부 
        public DateTime? UpdateDate { get; set; } 

        public DateTime InsertDate { get; set; } 
        
        [Column(TypeName = "char(1)")]
        public string IsDelete { get; set; } = "N";


        public NotebookSelfApproval(){
            //
        }
        public NotebookSelfApproval Clone() => (NotebookSelfApproval)MemberwiseClone();
        

    }
}

