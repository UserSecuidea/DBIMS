using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace WebVisit.Models
{
    public class VisitApplyPersonHistory
    {
        /*
        req: VisitApplyPersonHistoryID, VisitApplyPersonID, VisitApplyID, VisitDate, VisitorType, OrderNo, IsVisitorEdu, IsCleanEdu, IsTermsPrivarcy, VisitStatus, IsVIP, InsertVisitorType
        no req: VisitPersonID, Sabun, OrgID, OrgNameKor, OrgNameEng, VisitorEduDate, CleanEduDate, CleanEduScore, CarNo, CardID, CardNo, VipTypeCodeID, EntryDate, ExitDate, CleanEntryDate, CleanExitDate, InsertVisitPersonID, 
        
        [common]
        req: InsertDate, IsDelete
        no req: InsertSabun, InsertOrgID, InsertOrgNameKor, InsertOrgNameEng, UpdateDate, 
        */
        [Key]
        public int VisitApplyPersonHistoryID { get; set; }
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

        public int UpdateVisitorType { get; set; }
        public int? UpdateVisitPersonID { get; set; }


        //등록&변경자: 사번, 이름, 부서ID, 부서명
        // [Required] * 등록/변경자가 자가 방문자일 경우 UpdateSabun 대신에 UpdateVisitPersonID 가 들어감. 
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

        public VisitApplyPersonHistory(){
            //
        }
    }
}
