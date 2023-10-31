using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace WebVisit.Models
{
    public class CompanyHistory
    {
        /*req: CompanyHistoryID, CompanyID, CompanyType, BizRegNo, CompanyName, CeoName, CompanyStatus
        no req: Address, Tel, BizRegFile, ContactPersonName, Email, ContactPersonTel, 
        
        [common]
        req: UpdateSabun, InsertUpdateDate
        no req: UpdateOrgID, UpdateOrgNameKor, UpdateOrgNameEng 
        */

        [Key]
        public int CompanyHistoryID {get; set;}
        public int CompanyID { get; set; }
        public int CompanyType {get; set;}

        [Required(ErrorMessage = "Please enter a company biz no.")]
        [MaxLength(50)]
        public string BizRegNo { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Please enter a company name.")]
        [MaxLength(100)]
        public string CompanyName { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Please enter a ceo name.")]
        [MaxLength(50)]
        public string CeoName { get; set; } = string.Empty;
        
        [MaxLength(300)]
        public string? Address { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? Tel { get; set; } = string.Empty;
        
        [MaxLength(1000)]
        public string? BizFileData { get; set; } = string.Empty;

        [Column(TypeName = "varbinary(max)")]
        public byte[]? BizFileDataHash {get; set;}

        [MaxLength(50)]
        public string? ContactPersonName { get; set; } = string.Empty;
        
        [MaxLength(200)]
        public string? Email { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? ContactPersonTel { get; set; } = string.Empty;

        public int CompanyStatus {get; set;}

        //결재자: 사번, 성명, 부서ID, 부서명, 연락처 
        [MaxLength(50)]
        public string? ApprovalSabun { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? ApprovalName { get; set; } = string.Empty;

        // public int? ApprovalOrgID {get; set;}
        [MaxLength(50)]
        public string? ApprovalOrgID { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? ApprovalOrgNameKor { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? ApprovalOrgNameEng { get; set; } = string.Empty;
        
        [MaxLength(50)]
        public string? ApprovalTel { get; set; } = string.Empty;
        
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


        public CompanyHistory(){
            //
        }
    }
}