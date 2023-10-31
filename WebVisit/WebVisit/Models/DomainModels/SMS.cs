using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace WebVisit.Models
{
    public class SMS
    {
        /* 
        req: SMSID, SMSType, VisitorType, Mobile, SendDate
        no req: VisitPersonID, Sabun, OrgID, OrgNameKor, OrgNameEng
        
        [common]
        req: InsertSabun, InsertDate, IsDelete
        no req: InsertOrgID, InsertOrgNameKor, InsertOrgNameEng, UpdateDate, 
        */
        [Key]
        public int SMSID { get; set; }
        public int SMSType { get; set; }
        public int VisitorType { get; set; }
        public int? VisitPersonID { get; set; }

        //수신자 : 사번, 이름, 부서ID, 부서명
        // [Required] * 수신자가 방문자회원 일 경우 Sabun 대신에 VisitPersonID 가 들어감. 
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

        
        [Required]
        [MaxLength(50)]
        public string Mobile { get; set; } = string.Empty;
        public DateTime SendDate { get; set; } 


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

        public SMS(){
            //
        }

    }
}
