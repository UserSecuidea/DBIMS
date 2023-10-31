using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace WebVisit.Models
{
    public class BlackListHistory
    {
        /*
        req: BlackListHIstoryID, BlackListID, VisitorType, Name, Mobile, BlackListType, BlackListStatus, StartDate, EndDate
        no req: VisitPersonID, Sabun, OrgID, OrgNameKor, OrgNameEng, BirthDate, Gender, CompanyName, Tel, StatementFile, Reason, Memo
        
        [common]
        req: InsertUpdateDate
        no req: UpdateSabun, UpdateOrgID, UpdateOrgNameKor, UpdateOrgNameEng
        */
        [Key]
        public int BlackListHIstoryID { get; set; }
        public int BlackListID { get; set; }
        public int VisitorType { get; set; }
        public int? VisitPersonID { get; set; }

        //블랙리스트 회원: 사번, 이름, 부서ID, 부서명
        // [Required] * 블랙리스트 회원가 방문자회원 일 경우 Sabun 대신에 VisitPersonID 가 들어감. 
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
        

        [Column(TypeName = "char(10)")]
        public string? BirthDate { get; set; } = string.Empty;
        
        public int? Gender { get; set; }

        [MaxLength(50)]
        public string Mobile { get; set; } = string.Empty;

        [MaxLength(100)]
        public string CompanyName { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? Tel { get; set; } = string.Empty;

        public int BlackListType { get; set; }        


        [MaxLength(1000)]	
        public string? StatementFileData { get; set; } = string.Empty;

        [Column(TypeName = "varbinary(max)")]
        public byte[]? StatementFileDataHash {get; set;}


        [MaxLength(300)]
        public string? Reason { get; set; } = string.Empty;

        public int BlackListStatus { get; set; }

        public DateTime? StartDate { get; set; } 
        public DateTime? EndDate { get; set; } 

        [MaxLength(300)]
        public string? Memo { get; set; } = string.Empty;




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

        public BlackListHistory(){
            //
        }
    }
}
