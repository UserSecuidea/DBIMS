using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace WebVisit.Models
{
    public class VisitApplyPerson
    {
        /*
        req: VisitApplyPersonID, VisitApplyID, VisitDate, VisitorType, OrderNo, IsVisitorEdu, IsCleanEdu, IsTermsPrivarcy, VisitStatus, IsVIP, InsertVisitorType
        no req: VisitPersonID, Sabun, OrgID, OrgNameKor, OrgNameEng, VisitorEduDate, CleanEduDate, CleanEduScore, CarNo, CardID, CardNo, VipTypeCodeID, EntryDate, ExitDate, CleanEntryDate, CleanExitDate, InsertVisitPersonID, 
        
        [common]
        req: InsertDate, IsDelete
        no req: InsertSabun, InsertOrgID, InsertOrgNameKor, InsertOrgNameEng, UpdateDate, 
        */
        [Key]
        public int VisitApplyPersonID { get; set; }
        public int VisitApplyID { get; set; }
        
        [Column(TypeName = "char(10)")]
        public string VisitDate { get; set; } = string.Empty;
        
        public int VisitorType { get; set; }
        public int? VisitPersonID { get; set; }
         
        
        [Column(TypeName = "char(10)")]
        public string? BirthDate { get; set; } = string.Empty;
        // public DateTime? BirthDate { get; set; } 
        
        public int? Gender { get; set; }

        [MaxLength(50)]
        public string? Mobile { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? CompanyName { get; set; } = string.Empty;

        //방문자: 사번, 이름, 부서ID, 부서명
        // [Required] * 방문자가 방문자회원 일 경우 Sabun 대신에 VisitPersonID 가 들어감. 
        [MaxLength(50)]
        public string? Sabun { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? Name { get; set; } = string.Empty;
        
        // public int? OrgID {get; set;}
        [MaxLength(50)]
        public string? OrgID { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? OrgNameKor { get; set; } = string.Empty;
        [MaxLength(100)]
        public string? OrgNameEng { get; set; } = string.Empty;

        public int OrderNo { get; set; }

        public int? WorkApplyID { get; set; }

        public int? WorkVisitApplyID { get; set; }
        public int? SafetyEduID { get; set; }
        

        [Column(TypeName = "char(1)")]
        public string? IsVisitorEdu { get; set; } = "N";

        public DateTime? VisitorEduDate { get; set; } 
        
        [Column(TypeName = "char(1)")]
        public string? IsCleanEdu { get; set; } = "N";

        public DateTime? CleanEduDate { get; set; } 
        public int? CleanEduScore { get; set; }
        
        [Column(TypeName = "char(1)")]
        public string? IsSafetyEdu { get; set; } = "N";

        public DateTime? SafetyEduDate { get; set; } 


        [MaxLength(50)]
        public string? CarNo { get; set; } = string.Empty;

        [Column(TypeName = "char(1)")]
        public string? IsTermsPrivarcy { get; set; } = "N";

        [Column(TypeName = "char(1)")]
        public string? IsTermsVehicle { get; set; } = "N";        

        public int VisitApplyStatus { get; set; }
        public int VisitStatus { get; set; }
        
        public int? CardID { get; set; }
        [MaxLength(50)]
        public string? CardNo { get; set; } = string.Empty;

        [Column(TypeName = "char(1)")]
        public string? IsVIP { get; set; }
        
        public int? VipTypeCodeID { get; set; }
        public DateTime? EntryDate { get; set; } 
        public DateTime? ExitDate { get; set; } 
        public DateTime? CleanEntryDate { get; set; } 
        public DateTime? CleanExitDate { get; set; } 
        public int InsertVisitorType { get; set; }
        public int? InsertVisitPersonID { get; set; }


        //등록자: 사번, 이름, 부서ID, 부서명
        // [Required] * 등록자가 방문자일 경우 InsertSabun 대신에 VisitPersonID 가 들어감. 
        [Required]
        [MaxLength(50)]
        public string? InsertSabun { get; set; } = string.Empty;

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
        
        [Column(TypeName = "char(1)")]
        public string? LPRResult { get; set; } = "N";

        [NotMapped]
        public CarryItemDTO? CarryItemDTO { get; set; } // 휴대물품, 방문객 등록에서 사용

        [NotMapped]
        public VisitPerson? VisitPerson { get; set; } // 방문객 등록에서 사용

        [NotMapped]
        public VisitApplyPersonHistory? VisitApplyPersonHistory{ get; set;} // 방문객 등록에서 사용

        public VisitApplyPerson(){
            //
        }
        public VisitApplyPerson Clone() => (VisitApplyPerson)MemberwiseClone();
    }
}
