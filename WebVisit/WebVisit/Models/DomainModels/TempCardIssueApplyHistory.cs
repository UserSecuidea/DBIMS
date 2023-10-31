using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace WebVisit.Models
{
    public class TempCardIssueApplyHistory
    {
        /* 
        req: TempCardIssueApplyHistoryID, TempCardIssueApplyID, Sabun, TempCardIssueStatus
        no req: OrgID, OrgNameKor, OrgNameEng, CardID, CardNo, ReturnDate

        [common]
        req: UpdateSabun, InsertUpdateDate
        no req: UpdateOrgID, UpdateOrgNameKor, UpdateOrgNameEng
        */
        [Key]
        public int TempCardIssueApplyHistoryID { get; set; }
        public int TempCardIssueApplyID { get; set; }

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
        //임시증발급상태: 승인대기(0)/승인완료(1)/반려(2)/회수(3)
        public int TempCardIssueStatus { get; set; }
        //회수일시 
        public DateTime? ReturnDate { get; set; } 
        //출입증유효기간 
        public DateTime? CardValidDate { get; set; } 
        //재발급차수 
        public int? ReissueNo { get; set; }



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

        public TempCardIssueApplyHistory(){
            //
        }
        

    }
}