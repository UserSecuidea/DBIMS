using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace WebVisit.Models
{
    public class SafetyViolationPersonHistory
    {
        /*         
        req: SafetyViolationPersonHistoryID, SafetyViolationPersonID, SafetyViolationID, Sabun, ViolationTime, 
        no req: OrgID, OrgNameKor, OrgNameEng 

        [common]
        req: InsertUpdateDate
        no req:  
        */
        [Key]
        public int SafetyViolationPersonHistoryID { get; set; }
        public int SafetyViolationPersonID { get; set; }
        public int SafetyViolationID { get; set; }
        public int SafetyViolationReasonID { get; set; }

        //수칙위반자: 사번, 이름, 부서ID, 부서명
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

        [Column(TypeName = "char(10)")]
        public string BirthDate { get; set; } = string.Empty;       
        public int Gender { get; set; }

        [MaxLength(50)]
        public string Mobile { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a company name.")]
        [MaxLength(100)]
        public string CompanyName { get; set; } = string.Empty;

        public int ViolationTime { get; set; }

        public DateTime? StartDate { get; set; } 
        public DateTime? EndDate { get; set; } 
        
        //생성(갱신)일
        public DateTime InsertUpdateDate { get; set; } 

        public SafetyViolationPersonHistory(){}
    }
}
