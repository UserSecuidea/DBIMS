using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace WebVisit.Models
{
    public class VisitPerson
    {
        /*        
        req: VisitPersonID, Name, BirthDate, Gender, Mobile, CompanyName, RegVisitorType
        no req: GradeName, CarNo, Tel, VisitorEduLastDate, CleanEduLastDate, VisitLastDate
        
        [common]
        req: InsertSabun, InsertDate, IsDelete
        no req: InsertOrgID, InsertOrgNameKor, InsertOrgNameEng, UpdateDate, 
        */
        [Key]
        public int VisitPersonID { get; set; }
        
        [Required(ErrorMessage = "Please enter a name.")]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [Column(TypeName = "char(10)")]
        public string BirthDate { get; set; } = string.Empty;
        
        public int Gender { get; set; }

        [MaxLength(50)]
        public string Mobile { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a company name.")]
        [MaxLength(100)]
        public string CompanyName { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? GradeName { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? CarNo { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? Tel { get; set; } = string.Empty;

        public DateTime? VisitorEduLastDate { get; set; } 
        public DateTime? CleanEduLastDate { get; set; } 
        public DateTime? SafetyEduLastDate { get; set; } 
        public DateTime? VisitLastDate { get; set; } 

        public int RegVisitorType { get; set; }



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
        
        [MaxLength(1)]
        public string IsDelete { get; set; } = "N";

        public VisitPerson(){
            //
        }
        public VisitPerson Clone() => (VisitPerson)MemberwiseClone();
    }
}
