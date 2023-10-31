using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebVisit.Models
{
    public class Company
    {
        /*req: CompanyID, CompanyType, BizRegNo, CompanyName, CeoName, CompanyStatus
        no req: Address, Tel, BizRegFile, ContactPersonName, Email, ContactPersonTel, 
        
        [common]
        req: InsertSabun, InsertDate, IsDelete
        no req: InsertOrgID, InsertOrgNameKor, InsertOrgNameEng, UpdateDate, 
        */
        [Key]
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
        [JsonIgnore]
        public string? BizFileData { get; set; } = string.Empty;

        [Column(TypeName = "varbinary(max)")]
        [JsonIgnore]
        public byte[]? BizFileDataHash {get; set;}

        [NotMapped]
        [JsonIgnore]
        public List<IFormFile>? FormFile { get; set; }
        // public List<IFormFile>? FormFile { get; set; }

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

        public Company(){
            //
        }
        public Company Clone() => (Company)MemberwiseClone();
    }
}