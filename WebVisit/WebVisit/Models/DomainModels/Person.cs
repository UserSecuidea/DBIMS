using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace WebVisit.Models
{
    public class Person
    {
        /*
        req: PersonID, Sabun, Location, CompanyID, PersonTypeID, OrgID, LevelCodeID, Password, Name, Gender, Mobile, PersonStatusID, IsForeigner, IsAllowSMS
        no req: PID, OrgNameKor, OrgNameEng, GradeID, GradeName, PWUpdateDate, BirthDate, Tel, Email, ImageData, ImageDataHash, HomeAddr, HomeDetailAddr, HomeZipcode, Nationality, ImmStatusCodeID, 
        WorkTypeCodeID, CarAllowCnt, CarRegCnt, TermsPrivarcyFile, CardIssueStatus, CardCreateCnt, VisitorEduLastDate, CleanEduLastDate, SafetyEduLastDate, VisitLastDate, 
        ValidDate, UpdateIP, IsRecTempCard, TempCardRecDate, 
        
        [common]
        req: InsertSabun, InsertDate, IsDelete
        no req: InsertOrgID, InsertOrgNameKor, InsertOrgNameEng, UpdateDate, 
        */
        [Key]
        public int PersonID { get; set; }
        public int? PID { get; set; }

        [Required(ErrorMessage = "Please enter a sabun.")]
        [MaxLength(50)]
        public string Sabun { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a name.")]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
        
        [MaxLength(50)]
        public string? Location { get; set; } = string.Empty; // 사업장

        // public Company? Company{get;set;} // 삭제예정 
        public int? CompanyID { get; set; } = 0;

        [NotMapped]
        public string? CompanyName { get; set; } = string.Empty;
        
        [NotMapped]
        public string? PorteID { get; set; } = string.Empty; // V_USER_INFO

        // 임직원(0)/상주직원(1)/비상주관리자(2)/비상주직원(3)
        public int? PersonTypeID { get; set; } = 3;
        // public int? OrgID {get; set;}
        [MaxLength(50)]
        public string? OrgID { get; set; } = string.Empty;

        [NotMapped]
        public string? SapDeptCode { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? OrgNameKor { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? OrgNameEng { get; set; } = string.Empty;

        [Required]
        public int LevelCodeID { get; set; }

        public int? GradeID { get; set; }

        [MaxLength(100)]
        public string? GradeName { get; set; } = string.Empty;

        [Required]
        [MaxLength(300)]
        public string Password { get; set; } = string.Empty;

        public DateTime? PWUpdateDate { get; set; } 
        
        [Column(TypeName = "char(10)")]
        public string? BirthDate { get; set; } = string.Empty;
        // public DateTime? BirthDate { get; set; } 
        
        public int? Gender { get; set; }

        [MaxLength(50)]
        public string? Mobile { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? Tel { get; set; } = string.Empty;

        [MaxLength(200)]
        public string? Email { get; set; } = string.Empty;


        [MaxLength(1000)]
        public string? ImageData { get; set; } = string.Empty;

        [Column(TypeName = "varbinary(max)")]
        public byte[]? ImageDataHash {get; set;}
        
        public int? PersonStatusID { get; set; }

        [MaxLength(200)]
        public string? HomeAddr { get; set; } = string.Empty;
        [MaxLength(200)]
        public string? HomeDetailAddr { get; set; } = string.Empty;
        [MaxLength(10)]
        public string? HomeZipcode { get; set; } = string.Empty;

        public int? IsForeigner { get; set; }
        
        [MaxLength(50)]
        public string? Nationality { get; set; } = string.Empty;

        public int? ImmStatusCodeID { get; set; }

        
        [Column(TypeName = "char(10)")]
        public string? ImmStartDate { get; set; } = string.Empty;

        
        [Column(TypeName = "char(10)")]
        public string? ImmEndDate { get; set; } = string.Empty;
        
        [Required]
        [Column(TypeName = "char(1)")]
        public string IsAllowSMS { get; set; } = "Y";
        public int? WorkTypeCodeID { get; set; }

        public int? CarAllowCnt { get; set; } = 0;
        public int? CarRegCnt { get; set; } = 0;

        [MaxLength(1000)]	
        public string? TermsPrivarcyFileData { get; set; } = string.Empty;

        [Column(TypeName = "varbinary(max)")]
        public byte[]? TermsPrivarcyFileDataHash {get; set;}

        [NotMapped]
        public List<IFormFile>? FormFile { get; set; }
        [NotMapped]
        public List<IFormFile>? FormFile2 { get; set; }
        // * 출입증발급상태: 미발급(0)/발급(1)
        public int? CardIssueStatus { get; set; } = 0;
        public int? CardCreateCnt { get; set; } = 0;       
        
        public DateTime? VisitorEduLastDate { get; set; } 
        public DateTime? CleanEduLastDate { get; set; } 
        public DateTime? SafetyEduLastDate { get; set; } 
        // 최근방문일시 
        public DateTime? VisitLastDate { get; set; } 
        // 유효기간: 정규직일경우 2099년 12월 31일, 임시직은 상황에 맞게
        public DateTime? ValidDate { get; set; } 

        [MaxLength(50)]
        public string? UpdateIP { get; set; } = string.Empty;
        
        public int? CardID { get; set; }
        [MaxLength(50)]
        public string? CardNo { get; set; } = string.Empty;

        [Column(TypeName = "char(1)")]
        public string? IsRecTempCard { get; set; }

        public DateTime? TempCardRecDate { get; set; } 
        
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
        public string IsDelete { get; set; } = string.Empty;

        public Person(){
            //
        }

        public Person Clone() => (Person)MemberwiseClone();
    }
}