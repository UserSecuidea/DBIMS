using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace WebVisit.Models
{
    public class MealLog
    {
        [Key]
        public int MealLogID { get; set; }

        //식사자: 사번, 이름, 부서ID, 부서명
        [MaxLength(50)]
        public string Sabun { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? Name { get; set; } = string.Empty; 

        [MaxLength(100)]
        public string? OrgNameKor { get; set; } = string.Empty;
        [MaxLength(100)]
        public string? OrgNameEng { get; set; } = string.Empty;
        [MaxLength(100)]
        public string? GradeName { get; set; } = string.Empty;

        public DateTime MealYMD { get; set; } 
        
        [MaxLength(50)]
        public string? Location { get; set; } = string.Empty;

        //조식(1),중식(2),석식(3),야식(4), 비정상식수(0)
        public int? MealGB { get; set; }
        
        public int? Price { get; set; }
        public int? EqMasterID { get; set; }
        public int IsManual { get; set; }
        public int IsVisitor { get; set; }

        [MaxLength(50)]
        public string? VisitantName { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? VisitantCompany { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? VisitantGrade { get; set; } = string.Empty;

        [MaxLength(300)]
        public string? Comment { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? UpdateIP { get; set; } = string.Empty;

        [MaxLength(300)]
        public string? StateName { get; set; } = string.Empty;

        public int? CompanyID { get; set; }
        [MaxLength(100)]
        public string CompanyName { get; set; } = string.Empty;

        //등록자: 사번 (영양사 수기 등록시 입력 )
        [MaxLength(50)]
        public string? UpdateSabun { get; set; } = string.Empty; 

        //생성일
        public DateTime InsertDate { get; set; } 

        public MealLog(){
            //
        }
        

    }
}

