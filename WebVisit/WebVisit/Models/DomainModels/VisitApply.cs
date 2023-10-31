using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace WebVisit.Models
{
    public class VisitApply
    {
        /*
        req: VisitApplyID, Location, VisitStartDate, VisitEndDate, PlaceCodeID, VisitPurposeCodeID, ContactSabun, VisitorType, VisitApplyType, VisitApplyStatus, RegVisitorType
        no req: ContactOrgID, ContactOrgNameKor, ContactOrgNameEng, WorkApplyID, SafetyEduID, Purpose, VisitPersonID, 
        
        [common]
        req: InsertDate, IsDelete
        no req: InsertSabun, InsertOrgID, InsertOrgNameKor, InsertOrgNameEng, UpdateDate, 
        */
        [Key]
        public int VisitApplyID { get; set; }
        
        [MaxLength(50)]
        public string Location { get; set; } = string.Empty;
        // public int Location { get; set; }
        
        [Column(TypeName = "char(10)")]
        public string VisitStartDate { get; set; } = string.Empty;

        [Column(TypeName = "char(10)")]
        public string VisitEndDate { get; set; } = string.Empty;
        
        public int PlaceCodeID { get; set; }
        public int? VisitPurposeCodeID { get; set; }
        public int? VisitManualPurposeCodeID { get; set; }

        //접견자: 사번, 이름, 부서ID, 부서명
        [Required]
        [MaxLength(50)]
        public string ContactSabun { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? ContactName { get; set; } = string.Empty;
        
        // public int? ContactOrgID {get; set;}
        [MaxLength(50)]
        public string? ContactOrgID { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? ContactOrgNameKor { get; set; } = string.Empty;
        [MaxLength(100)]
        public string? ContactOrgNameEng { get; set; } = string.Empty;

        // public int VisitorType { get; set; }
        public int VisitApplyType { get; set; }

        public int? WorkApplyID { get; set; }

        public int? WorkVisitApplyID { get; set; }
        public int? SafetyEduID { get; set; }
        public int VisitApplyStatus { get; set; }

        [MaxLength(300)]
        public string? Purpose { get; set; } = string.Empty;
        public int RegVisitorType { get; set; }
        public int? VisitPersonID { get; set; }


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

        public VisitApply(){
            //
        }

        public VisitApply Clone() => (VisitApply)MemberwiseClone();
    }
}
