using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace WebVisit.Models
{
    public class VisitApplyHistory
    {
        /*
        req: VisitApplyID, Location, VisitStartDate, VisitEndDate, PlaceCodeID, VisitPurposeCodeID, ContactSabun, VisitorType, VisitApplyType, VisitApplyStatus, RegVisitorType
        no req: ContactOrgID, ContactOrgNameKor, ContactOrgNameEng, WorkApplyID, SafetyEduID, Purpose, RejectReason, VisitPersonID, 
        
        [common]
        req: InsertUpdateDate
        no req: UpdateSabun, UpdateOrgID, UpdateOrgNameKor, UpdateOrgNameEng 
        */
        [Key]
        public int VisitApplyHistoryID { get; set; }
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


        [MaxLength(300)]
        public string? RejectReason { get; set; } = string.Empty;


        //등록&변경자: 사번, 이름, 부서ID, 부서명
        // [Required] * 등록/변경자가 자가 방문자일 경우 UpdateSabun 대신에 VisitPersonID 가 들어감. 
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


        public VisitApplyHistory(){
            //
        }
    }
}

