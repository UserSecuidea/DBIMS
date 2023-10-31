using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace WebVisit.Models
{
    public class CardIssueApplyHistory
    {
        /* 
        req: CardIssueApplyHistoryID, CardIssueApplyID, Sabun, TermsPrivarcyFile, ContactSabun, CardTypeID, CardIssueType, CardApplyStatus
        no req: OrgID, OrgNameKor, OrgNameEng, ContactOrgID, ContactOrgNameKor, ContactOrgNameEng, CardID, CardNo, ReissueNo, ApplyReason, Memo

        [common]
        req: UpdateSabun, InsertUpdateDate
        no req: UpdateOrgID, UpdateOrgNameKor, UpdateOrgNameEng 
        */
        [Key]
        public int CardIssueApplyHistoryID { get; set; }
        public int CardIssueApplyID { get; set; }

        //#### 신청자정보 ####
        [MaxLength(50)]
        public string? Location { get; set; } = string.Empty; // 사업장
        public int? CompanyID { get; set; }
        [MaxLength(100)]
        public string CompanyName { get; set; } = string.Empty;
        //회원구분: 임직원(0)/상주직원(1)/비상주관리자(2)/비상주직원(3)
        public int? PersonTypeID { get; set; }

        //사원: 사번, 이름, 부서ID, 부서명
        [Required]
        [MaxLength(50)]
        public string Sabun { get; set; } = string.Empty;
        [MaxLength(50)]
        public string? Name { get; set; } = string.Empty;
        // public int? OrgID {get; set;}
        [MaxLength(50)]
        public string? OrgID { get; set; } = string.Empty;
      
        [MaxLength(100)]
        public string? OrgNameKor { get; set; } = string.Empty;
        [MaxLength(100)]
        public string? OrgNameEng { get; set; } = string.Empty;
        public int? GradeID { get; set; }
        [MaxLength(100)]
        public string? GradeName { get; set; } = string.Empty;
        [MaxLength(50)]
        public string? Mobile { get; set; } = string.Empty;
        [MaxLength(50)]
        public string? Tel { get; set; } = string.Empty; 
        [MaxLength(1000)]	
        public string? TermsPrivarcyFileData { get; set; } = string.Empty;
        [Column(TypeName = "varbinary(max)")]
        public byte[]? TermsPrivarcyFileDataHash {get; set;}


        //#### 담당자정보 ####
        //담당자: 사번, 이름, 부서ID, 부서명
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


        //출입증발급구분: 신규(0)/재발급(1)
        public int CardIssueType { get; set; }
        [MaxLength(300)]
        public string? ApplyReason { get; set; } = string.Empty;
        

        //#### 출입증 정보 ####
        //출입증ID 
        public int? CardID { get; set; }
        //출입증번호 
        [MaxLength(50)]
        public string? CardNo { get; set; } = string.Empty;
        //출입증구분: 0-일반(사원증, 상주사원증, 비상주사원증), 1-방문증(일반방문증/공사출입증)
        public int CardTypeID { get; set; }
        //출입증명 
        [MaxLength(50)]
        public string? CardName { get; set; } = string.Empty;
        //출입증 신청상태: 승인대기(0), 승인완료(1), 반려(2) 
        public int CardApplyStatus { get; set; }
        //출입증발급상태: 미발급(0)/발급(1)
        public int? CardIssueStatus { get; set; } = 0; 
        //출입증유효기간 
        public DateTime? CardValidDate { get; set; } 
        //재발급차수 
        public int? ReissueNo { get; set; }

        //처리사유 
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

        public CardIssueApplyHistory(){
            //
        }
        

    }
}
